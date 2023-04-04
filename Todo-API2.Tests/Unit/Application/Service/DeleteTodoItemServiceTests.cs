using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_API2.Application.Mapping;
using Todo_API2.Application.Services;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Presentation.Dtos;

namespace Todo_API2.Tests.Unit.Application.Service
{
    public class DeleteTodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly IMapper _mapper;

        public DeleteTodoItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
            _mockRepository = new Mock<ITodoItemRepository>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTodoItemDto_WhenValidIdIsProvided()
        {
            // Arrange
            long validId = 1;
            TodoItems todoItem = new TodoItems
            {
                Id = validId,
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };

            _mockRepository.Setup(x => x.GetByIdAsync(validId)).ReturnsAsync(todoItem);

            var service = new TodoItemService(_mockRepository.Object, _mapper);

            // Act
            var result = await service.DeleteAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TodoItemDTO>(result);
            Assert.Equal(validId, result.Id);
        }
    }
}
