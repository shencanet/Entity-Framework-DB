using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("tasksDB"));
builder.Services.AddDbContext<TasksContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("psql"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TasksContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    // return Results.Ok("Databse in memory runing: " + dbContext.Database.IsInMemory());
    return Results.Ok("Database in postgreSQL runing: " + dbContext.Database.IsNpgsql());
});

app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) => {
    return Results.Ok(dbContext.Tasks.Include(p => p.Category));
});

app.MapGet("/api/tasks-priority-low", async ([FromServices] TasksContext dbContext) => {
    return Results.Ok(dbContext.Tasks.Include(p => p.Category).Where(p => p.PriorityTask == projectef.Models.Priority.Low));
});

app.MapPost("/api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task) => {

    task.TaskId = Guid.NewGuid();
    task.CreatedAt =  DateTime.Now;

    // await dbContext.AddAsync(task);
    await dbContext.Tasks.AddAsync(task);

    await dbContext.SaveChangesAsync();

    return Results.Created("/api/tasks", null);

});

app.MapPut("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task, [FromRoute] Guid id) => {

    var taskFound =  dbContext.Tasks.Find(id);

    if(taskFound is null) return Results.NotFound();

    taskFound.CategoryId = task.CategoryId != null ? task.CategoryId : taskFound.CategoryId;
    taskFound.Title = task.Title != null ? task.Title : taskFound.Title;
    taskFound.Description = task.Description != null ? task.Description : taskFound.Description;
    taskFound.PriorityTask = task.PriorityTask != 0 ? task.PriorityTask : taskFound.PriorityTask;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) => {

    var taskFound =  dbContext.Tasks.Find(id);

    if(taskFound is null) return Results.NotFound();

    dbContext.Remove(taskFound);
    
    await dbContext.SaveChangesAsync();

    return Results.NoContent();

});

app.Run();