using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;

public class UpdateTodoListDTO
{
    [MaxLength(50, ErrorMessage = "List name must be 50 characters or less"), MinLength(1)]
    public string? Name { get; set; }
}
