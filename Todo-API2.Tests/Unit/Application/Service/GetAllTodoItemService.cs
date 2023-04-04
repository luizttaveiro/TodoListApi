using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_API2.Application.Interfaces;
using Todo_API2.Application.Mapping;
using Todo_API2.Application.Services;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Presentation.Dtos;

namespace Todo_API2.Tests.Unit.Application.Service
{
    public class TodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ITodoItemService _service;

        public TodoItemServiceTests()
        {
            _mockRepository = new Mock<ITodoItemRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _service = new TodoItemService(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsTodoItems()
        {
            // Arrange
            var todoItems = new List<TodoItems>
        {
            new TodoItems { Id = 1, Title = "Test Item 1", Description = "This is test item 1", Status = TodoItemStatus.NotStarted },
            new TodoItems { Id = 2, Title = "Test Item 2", Description = "This is test item 2", Status = TodoItemStatus.Doing },
            new TodoItems { Id = 3, Title = "Test Item 3", Description = "This is test item 3", Status = TodoItemStatus.Blocked }
        };
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(todoItems);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsType<List<TodoItemDTO>>(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoTodoItems()
        {
            // Arrange
            var todoItems = new List<TodoItems>();
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(todoItems);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsType<List<TodoItemDTO>>(result);
            Assert.Empty(result);
        }
    }
}
