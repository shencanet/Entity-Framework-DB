using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(categoria=>{
            categoria.ToTable("Categoria");
            categoria.HasKey(p=>p.CategoriaId);
            categoria.Property(c=>c.Nombre).HasMaxLength(150).IsRequired();
            categoria.Property(p=>p.Descripcion).HasMaxLength(500);
            categoria.Property(p=>p.Peso).HasDefaultValue(0);
       });

        modelBuilder.Entity<Tarea>(tarea=>{
            tarea.ToTable("Tarea");
            tarea.HasKey(p=>p.TareaId);
            tarea.Property(p=>p.Titulo).HasMaxLength(200).IsRequired();
            tarea.Property(p=>p.Descripcion).HasMaxLength(500);
            tarea.Property(p=>p.FechaCreacion).HasDefaultValueSql("getdate()");
            tarea.Property(p=>p.PrioridadTarea).HasDefaultValue(Prioridad.Baja);
            tarea.HasOne(p=>p.Categoria).WithMany(p=>p.Tareas).HasForeignKey(p=>p.CategoriaId);
        });
    }
}