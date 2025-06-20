using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;

public class CreateTodoListDTO
{
    [Required]
    [MaxLength(50, ErrorMessage = "List name must be 50 characters or less"), MinLength(1)]
    public required string Name { get; set; }
}
