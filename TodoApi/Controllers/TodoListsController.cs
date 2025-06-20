using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/todolists")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/todolists
        [HttpGet]
        public async Task<ActionResult<IList<TodoList>>> GetTodoLists()
        {
            var lists = await _context.TodoList
            .Include(l => l.Items)
            .Select(l => new TodoList
            {
                Id = l.Id,
                Name = l.Name,
                Items = l.Items.Select(i => new TodoItem
                {
                    Id = i.Id,
                    Description = i.Description,
                    IsCompleted = i.IsCompleted
                }).ToList()
            })
            .ToListAsync();

            return Ok(lists);
        }

        // GET: api/todolists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
            var todoList = await _context.TodoList
                .Include(l => l.Items)
                .Select(l => new TodoList
                {
                    Id = l.Id,
                    Name = l.Name,
                    Items = l.Items.Select(i => new TodoItem
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsCompleted = i.IsCompleted
                    }).ToList()
                })
                .FirstOrDefaultAsync(l => l.Id == id);

            if (todoList == null)
            {
                return NotFound($"No existe lista con el id {id}");
            }

            return Ok(todoList);
        }

        // PUT: api/todolists/5
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoList(int id, string descripcion)
        {
            var todoList = await _context.TodoList.FindAsync(id);

            if (todoList == null)
            {
                return NotFound($"No existe lista con el id {id}");
            }

            todoList.Name = descripcion;
            await _context.SaveChangesAsync();

            return Ok(todoList);
        }

        // POST: api/todolists
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoList>> PostTodoList(CreateTodoListDTO payload)
        {
            var todoList = new TodoList { Name = payload.Name };

            _context.TodoList.Add(todoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoList", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/todolists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoList(int id)
        {
            var todoList = await _context.TodoList
                .Include(l => l.Items)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (todoList == null)
            {
                return NotFound();
            }

            // Elimino los items de esa lista
            _context.TodoItem.RemoveRange(todoList.Items);

            // Elimino la lista
            _context.TodoList.Remove(todoList);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Se elimino la lista y sus items con exito" });
        }

        private bool TodoListExists(long id)
        {
            return (_context.TodoList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
