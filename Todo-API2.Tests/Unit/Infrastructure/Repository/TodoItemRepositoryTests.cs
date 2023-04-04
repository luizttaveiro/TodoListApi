using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_API2.Application.Mapping;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;
using Todo_API2.Infrastructure.Data;
using Todo_API2.Infrastructure.Repositories;
using Xunit.Extensions;

namespace Todo_API2.Tests.Unit.Infrastructure.Repository
{
    public class TodoItemRepositoryTests
    {
        private readonly DbContextOptions<TodoContext> _options;
        private readonly IMapper _mapper;
        private TodoContext _context;
        private TodoItemRepository _repository;

        public TodoItemRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TodoItemDatabase")
                .Options;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            _mapper = configuration.CreateMapper();
            _context = new TodoContext(_options);
            _repository = new TodoItemRepository(_context);
        }
        
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTodoItems()
        {
            // Arrange
            using (var context = new TodoContext(_options))
            {
                var todoItem1 = new TodoItems { Title = "Todo Item 1", Description = "Description 1", Status = TodoItemStatus.NotStarted };
                var todoItem2 = new TodoItems { Title = "Todo Item 2", Description = "Description 2", Status = TodoItemStatus.Doing };
                var todoItem3 = new TodoItems { Title = "Todo Item 3", Description = "Description 3", Status = TodoItemStatus.Done };
                context.AddRange(todoItem1, todoItem2, todoItem3);
                await context.SaveChangesAsync();
            }

            using (var context = new TodoContext(_options))
            {
                var repository = new TodoItemRepository(context);

                // Act
                var result = await repository.GetAllAsync();

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<TodoItems>>(result);
                Assert.Equal(4, result.Count());
            }
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectTodoItem_WhenGivenValidId()
        {
            // Arrange
            using var context = new TodoContext(_options);
            var repository = new TodoItemRepository(context);

            var todoItem = new TodoItems
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            context.TodoItems.Add(todoItem);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(todoItem.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoItem.Id, result.Id);
            Assert.Equal(todoItem.Title, result.Title);
            Assert.Equal(todoItem.Description, result.Description);
            Assert.Equal(todoItem.Status, result.Status);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenGivenInvalidId()
        {
            // Arrange
            using var context = new TodoContext(_options);
            var repository = new TodoItemRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTodoItem_WhenValidTodoItemIsProvided()
        {
            // Arrange
            var todoItem = new TodoItems
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            using (var context = new TodoContext(_options))
            {
                var repository = new TodoItemRepository(context);

                // Act
                var createdTodoItem = await repository.CreateAsync(todoItem);
                await context.SaveChangesAsync();

                // Assert
                var savedTodoItem = await context.TodoItems.FirstOrDefaultAsync(t => t.Id == createdTodoItem.Id);
                Assert.NotNull(savedTodoItem);
                Assert.Equal(createdTodoItem.Title, savedTodoItem.Title);
                Assert.Equal(createdTodoItem.Description, savedTodoItem.Description);
                Assert.Equal(createdTodoItem.Status, savedTodoItem.Status);
            }
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowDbUpdateException_WhenInvalidTodoItemIsProvided()
        {
            // Arrange
            var todoItem = new TodoItems();
            using (var context = new TodoContext(_options))
            {
                var repository = new TodoItemRepository(context);

                // Act & Assert
                await Assert.ThrowsAsync<DbUpdateException>(() => repository.CreateAsync(todoItem));
            }
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTodoItemInDatabase()
        {
            // Arrange
            var todoItem = new TodoItems
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            // Act
            todoItem.Title = "Updated Todo Item";
            await _repository.UpdateAsync(todoItem);

            // Assert
            var updatedTodoItem = await _context.TodoItems.FindAsync(todoItem.Id);
            Assert.Equal(todoItem.Title, updatedTodoItem?.Title);
            Assert.Equal(todoItem.Description, updatedTodoItem?.Description);
            Assert.Equal(todoItem.Status, updatedTodoItem.Status);
        }


        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenTodoItemDoesNotExist()
        {
            // Arrange
            long id = 1;

            // Act
            var result = await _repository.DeleteAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnDeletedTodoItem_WhenTodoItemExists()
        {
            // Arrange
            var todoItem = new TodoItems
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(todoItem.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoItem.Id, result.Id);
            Assert.Equal(todoItem.Title, result.Title);
            Assert.Equal(todoItem.Description, result.Description);
            Assert.Equal(todoItem.Status, result.Status);

            var deletedItem = await _context.TodoItems.FindAsync(todoItem.Id);
            Assert.Null(deletedItem);
        }
    }
}