using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; } = false;

    [ForeignKey("TodoList")]
    public int TodoListId { get; set; }
    
    public TodoList? TodoList { get; set; }
}
