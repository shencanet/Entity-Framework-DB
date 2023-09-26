using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }
    
    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Trabajo" },
            new Categoria { Id = 2, Nombre = "Estudios" },
            new Categoria { Id = 3, Nombre = "Personal" }
        );
    }
}