using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos
{
    public class CreateTodoItemDTO
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Description must be 200 characters or less"), MinLength(1)]
        public required string Description { get; set; }

        [Required]
        public int TodoListId { get; set; }
    }
}
