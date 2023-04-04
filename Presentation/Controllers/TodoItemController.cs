using log4net;
using Microsoft.AspNetCore.Mvc;
using Todo_API2.Application.Interfaces;
using Todo_API2.Presentation.Dtos;

namespace Todo_API2.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;
        private static readonly ILog _logger = LogManager.GetLogger("Todo Item Service");

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllAsync()
        {
            var todoItems = await _service.GetAllAsync();
            return Ok(todoItems);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetByIdAsync(long id)
        {
            var todoItem = await _service.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateAsync(TodoItemDTO todoItemDTO)
        {
            var createdTodoItem = await _service.CreateAsync(todoItemDTO);
            return Created("CreateTodoItem", createdTodoItem);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(long id, TodoItemDTO todoItemDTO)
        {
            await _service.UpdateAsync(id, todoItemDTO);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemDTO>> DeleteAsync(long id)
        {
            var todoItem = await _service.DeleteAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

    }
}
