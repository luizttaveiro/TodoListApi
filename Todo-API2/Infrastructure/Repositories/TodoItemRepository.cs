using log4net;
using Microsoft.EntityFrameworkCore;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Infrastructure.Data;

namespace Todo_API2.Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;
        private static readonly ILog _logger = LogManager.GetLogger("***** TodoItemRepository *****");

        public TodoItemRepository(TodoContext todoContext)
        {
            _context = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
        }

        public async Task<IEnumerable<TodoItems>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItems> GetByIdAsync(long id)
        {
            try
            {
                return await _context.TodoItems.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new KeyNotFoundException();
            }
        }

        public async Task<TodoItems> CreateAsync(TodoItems todoItem)
        {
            try
            {
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                return todoItem;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new DbUpdateException();
            }

        }

        public async Task UpdateAsync(TodoItems todoItem)
        {
            try
            {
                _context.Entry(todoItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new DbUpdateException();
            }

        }

        public async Task<TodoItems> DeleteAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
    }
}
