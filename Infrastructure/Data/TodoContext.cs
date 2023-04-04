using Microsoft.EntityFrameworkCore;
using Todo_API2.Domain.Entities;

namespace Todo_API2.Infrastructure.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItems> TodoItems { get; set; }
    }
}
