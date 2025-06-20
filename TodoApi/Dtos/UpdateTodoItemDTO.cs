using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos
{
    public class UpdateTodoItemDTO
    {
        [MaxLength(200, ErrorMessage = "Description must be 200 characters or less"), MinLength(1)]
        public string? Description { get; set; }

        public bool? IsCompleted { get; set; } 
    }
}
