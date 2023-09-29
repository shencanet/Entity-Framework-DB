using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
//builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));
builder.Services.AddDbContext<TareasContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("cnTareas")));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async([FromServices] TareasContext dbContext) =>
{

    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());

});

app.MapGet("/api/tareas", async([FromServices] TareasContext dbContext) => 
{
    return Results.Ok(dbContext.Tareas.Include(p=>p.Categoria).Where(p => p.PrioridadTarea == projectef.Models.Priority.Alta));
});

app.MapPost("/api/tareas", async([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) => 
{
    tarea.TareaID = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea);
    //await dbContext.Tareas.AddAsync(tarea);//Otra forma de agregar

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) => 
{

    var tareaActual = dbContext.Tareas.Find(id);

    if(tareaActual!=null){

        tareaActual.CategoriaID = tarea.CategoriaID;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async([FromServices] TareasContext dbContext, [FromRoute] Guid id) => 
{

    var tareaActual = dbContext.Tareas.Find(id);

    if(tareaActual!=null){

        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapGet("/api/categorias", async([FromServices] TareasContext dbContext) => 
{
    return Results.Ok(dbContext.Categorias);
});

app.Run();