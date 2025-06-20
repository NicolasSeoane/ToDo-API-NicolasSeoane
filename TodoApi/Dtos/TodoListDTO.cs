namespace TodoApi.Dtos
{
    public class TodoListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TodoItemDTO> Items { get; set; } = new();
    }
}
