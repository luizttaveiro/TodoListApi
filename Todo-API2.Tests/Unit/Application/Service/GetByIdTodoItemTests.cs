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

namespace Todo_API2.Tests.Unit.Application.Service
{
    public class GetByIdTodoItemTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly IMapper _mapper;

        public GetByIdTodoItemTests()
        {
            _mockRepository = new Mock<ITodoItemRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectTodoItem_WhenItemExists()
        {
            // Arrange
            var expectedTodoItem = new TodoItems { Id = 1, Title = "Test Todo Item", Description = "This is a test todo item", Status = TodoItemStatus.NotStarted };
            _mockRepository.Setup(repo => repo.GetByIdAsync(expectedTodoItem.Id))
                .ReturnsAsync(expectedTodoItem);
            var service = new TodoItemService(_mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByIdAsync(expectedTodoItem.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTodoItem.Id, result.Id);
            Assert.Equal(expectedTodoItem.Title, result.Title);
            Assert.Equal(expectedTodoItem.Description, result.Description);
            Assert.Equal(expectedTodoItem.Status, result.Status);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((TodoItems)null);
            var service = new TodoItemService(_mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}
