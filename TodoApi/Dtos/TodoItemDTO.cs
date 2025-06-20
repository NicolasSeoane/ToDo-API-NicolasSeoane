namespace TodoApi.Dtos
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public string? TodoListName { get; set; } = string.Empty;
        public int TodoListId { get; set; } = 0;    
    }
}
