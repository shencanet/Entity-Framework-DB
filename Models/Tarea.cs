using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectef.Models;

public class Tarea{

    //[Key]
    public Guid TareaID{get;set;}

    [ForeignKey("CategoriaID")]
    public Guid CategoriaID{get;set;}
    
    //[Required]
    //[MaxLength(200)]
    public string Titulo { get; set; }
    public string Descripcion { get; set; }

    public Priority PrioridadTarea { get; set; }
    public DateTime FechaCreacion { get; set; }
    public virtual Categoria Categoria { get; set; }

    //[NotMapped]
    public string Resumen { get; set; }

}

public enum Priority{

    Baja,
    Media,
    Alta

}