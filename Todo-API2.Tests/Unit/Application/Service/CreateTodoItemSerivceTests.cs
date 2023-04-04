using AutoMapper;
using Moq;
using Todo_API2.Application.Mapping;
using Todo_API2.Application.Services;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Presentation.Dtos;


namespace Todo_API2.Tests.Unit.Application.Service
{
    public class CreateTodoItemSerivceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly IMapper _mapper;
        public CreateTodoItemSerivceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            _mockRepository = new Mock<ITodoItemRepository>();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTodoItem_WhenValidInputIsProvided()
        {
            // Arrange
            var service = new TodoItemService(_mockRepository.Object, _mapper);
            var todoItemDTO = new TodoItemDTO
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            var todoItem = new TodoItems
            {
                Id = 1,
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<TodoItems>()))
                .ReturnsAsync(todoItem);

            // Act
            var result = await service.CreateAsync(todoItemDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoItemDTO.Title, result.Title);
            Assert.Equal(todoItemDTO.Description, result.Description);
            Assert.Equal(todoItemDTO.Status, result.Status);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var service = new TodoItemService(_mockRepository.Object, _mapper);
            var todoItemDTO = new TodoItemDTO
            {
                Title = "Test Todo Item",
                Description = "This is a test todo item",
                Status = TodoItemStatus.NotStarted
            };
            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<TodoItems>()))
                .Throws(new Exception("Failed to create todo item"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(todoItemDTO));
        }
    }
}
