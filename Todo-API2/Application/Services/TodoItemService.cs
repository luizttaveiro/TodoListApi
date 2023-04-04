using Todo_API2.Application.Interfaces;
using Todo_API2.Application.Mapping;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Presentation.Dtos;
using AutoMapper;

namespace Todo_API2.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO> GetByIdAsync(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItems>(todoItemDTO);
            var createdTodoItem = await _repository.CreateAsync(todoItem);
            return _mapper.Map<TodoItemDTO>(createdTodoItem);
        }

        public async Task<TodoItemDTO> DeleteAsync(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);
            var todoItemEntity = _mapper.Map<TodoItemDTO>(todoItem); // convert DTO to entity
            await _repository.DeleteAsync(todoItemEntity.Id);
            return _mapper.Map<TodoItemDTO>(todoItemEntity); // convert entity back to DTO

        }

        public async Task UpdateAsync(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                throw new ArgumentException($"Todo item with ID {id} not found");
            }
            _mapper.Map(todoItemDTO, todoItem);
            await _repository.UpdateAsync(todoItem);
        }

    }
}
