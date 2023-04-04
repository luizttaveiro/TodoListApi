using AutoMapper;
using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;
using Todo_API2.Presentation.Dtos;

namespace Todo_API2.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItems, TodoItemDTO>();
            CreateMap<TodoItemDTO, TodoItems>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TodoItemStatus>(src.Status.ToString())))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<TodoItemDTO, TodoItems>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TodoItemStatus>(src.Status.ToString())))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<TodoItemDTO, TodoItems>()
                .ConvertUsing<CreateTodoItemConverter>();

        }

        public class CreateTodoItemConverter : ITypeConverter<TodoItemDTO, TodoItems>
        {
            public TodoItems Convert(TodoItemDTO source, TodoItems destination, ResolutionContext context)
            {
                return new TodoItems
                {
                    Title = source.Title,
                    Description = source.Description,
                    Status = Enum.Parse<TodoItemStatus>(source.Status.ToString())
                };
            }
        }
    }
}