using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }


        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CreateTodoItemDTO dto)
        {

            // Verifica si existe la lista a la que se quiere asociar
            var todoListExists = await _context.TodoList.AnyAsync(tl => tl.Id == dto.TodoListId);
            if (!todoListExists)
            {
                return NotFound($"No existe una lista con el ID {dto.TodoListId}.");
            }

            var newItem = new TodoItem
            {
                Description = dto.Description,
                TodoListId = dto.TodoListId
            };

            _context.TodoItem.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = newItem.Id }, newItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ChangeDescription")]
        public async Task<IActionResult> PutTodoItemDescription(int id, string todoItemDescription)
        {
            //verifico si exite un item con ese ID
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null)
            {
                return NotFound($"No existe un item con el ID {id}.");
            }

            //actualizo la descripcion
            if (todoItemDescription != null)
            {
                item.Description = todoItemDescription;
            }


            _context.TodoItem.Update(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Descripción actualizada con éxito" });
        }

        [HttpPut("MarkAsCompleted")]
        public async Task<IActionResult> PutTodoItemCompleted(int id)
        {
            //verifico si exite un item con ese ID
            var item = await _context.TodoItem.FindAsync(id);
            if (item == null)
            {
                return NotFound($"No existe un item con el ID {id}.");
            }

            //si el item.IsCompleted es falso, lo cambio a true else no hago nada
            if (!item.IsCompleted)
            {
                item.IsCompleted = true;
                _context.TodoItem.Update(item);
                await _context.SaveChangesAsync();
            }
            
            return Ok(new { message = "Item completado con éxito" });
        }



        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItem()
        {
            return await _context.TodoItem
                .Include(t => t.TodoList)
                .Select(t => new TodoItemDTO
                {
                    Id = t.Id,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    TodoListName = t.TodoList != null ? t.TodoList.Name : null,
                    TodoListId = t.TodoListId
                })
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }


        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound($"No existe item con el id {id}");
            }

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item eliminado con éxito" });
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
    }
}
