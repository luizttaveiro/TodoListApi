using Todo_API2.Domain.Entities;

namespace Todo_API2.Domain.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItems>> GetAllAsync();
        Task<TodoItems> GetByIdAsync(long id);
        Task<TodoItems> CreateAsync(TodoItems todoItem);
        Task UpdateAsync(TodoItems todoItem);
        Task<TodoItems> DeleteAsync(long id);
    }
}
