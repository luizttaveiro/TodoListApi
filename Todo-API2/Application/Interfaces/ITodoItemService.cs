using Todo_API2.Presentation.Dtos;

namespace Todo_API2.Application.Interfaces
{
    public interface ITodoItemService
    {
        Task<TodoItemDTO> CreateAsync(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> GetByIdAsync(long id);
        Task<IEnumerable<TodoItemDTO>> GetAllAsync();
        Task UpdateAsync(long id, TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> DeleteAsync(long id);
    }
}
