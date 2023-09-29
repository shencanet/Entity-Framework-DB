using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class TareasContext : DbContext {

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        List<Categoria> categoriasInit = new List<Categoria>();

        categoriasInit.Add(new Categoria(){ CategoriaId = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e9c"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria(){ CategoriaId = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e8c"), Nombre = "Actividades personales", Peso = 50});

        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property( p=> p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p=>p.Descripcion).IsRequired(false);
            categoria.Property(p=>p.Peso);
            
            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea(){ TareaID = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e7c"), 
                                    CategoriaID = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e9c"), 
                                    PrioridadTarea = Priority.Media,
                                    Titulo = "Pago de servicios publicos",
                                    FechaCreacion = DateTime.Now});
                                
        tareasInit.Add(new Tarea(){ TareaID = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e6c"), 
                                    CategoriaID = Guid.Parse("1ebc42cb-da81-46c1-93cb-8291f5442e8c"), 
                                    PrioridadTarea = Priority.Baja,
                                    Titulo = "Terminar serie en netflix",
                                    FechaCreacion = DateTime.Now});
        

        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("tarea");
            tarea.HasKey(p => p.TareaID);

            tarea.HasOne(p => p.Categoria).WithMany(p=>p.Tareas).HasForeignKey(p=>p.CategoriaID);

            tarea.Property( p=> p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p=>p.Descripcion).IsRequired(false);
            tarea.Property(p=>p.PrioridadTarea);
            tarea.Property(p=>p.FechaCreacion);
            tarea.Ignore(p=>p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}