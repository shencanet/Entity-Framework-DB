using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectef.Models;

public class Task
{
    // [Key]
    public Guid TaskId {get;set;}

    // [ForeignKey("CategoryId")]
    public Guid CategoryId {get;set;}

    // [Required]
    // [MaxLength(200)]
    public string Title {get;set;}
    public string Description {get;set;}
    public Priority PriorityTask {get;set;}
    public DateTime CreatedAt {get;set;}
    public virtual Categoria Category {get;set;}

    // [NotMapped]
    public string Resume {get;set;}
}

public enum Priority
{
    Low,
    Medium,
    High
}