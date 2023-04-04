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
    public class UpdateTodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly IMapper _mapper;

        public UpdateTodoItemServiceTests()
        {
            _mockRepository = new Mock<ITodoItemRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowArgumentException_WhenInvalidIdIsProvided()
        {
            // Arrange
            long id = 1;
            TodoItems todoItem = null;
            var todoItemDTO = new TodoItemDTO
            {
                Id = id,
                Title = "Updated Test Todo Item",
                Description = "This is an updated test todo item",
                Status = TodoItemStatus.Done
            };
            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(todoItem);
            var service = new TodoItemService(_mockRepository.Object, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(id, todoItemDTO));
        }
    }
}
