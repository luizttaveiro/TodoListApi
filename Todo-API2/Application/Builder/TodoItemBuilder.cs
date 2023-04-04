using Todo_API2.Domain.Entities;
using Todo_API2.Domain.Enums;

namespace Todo_API2.Application.Builder
{
    public class TodoItemBuilder
    {
        private string _title;
        private string _description;
        private TodoItemStatus _status;

        public TodoItemBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public TodoItemBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TodoItemBuilder WithStatus(TodoItemStatus status)
        {
            _status = status;
            return this;
        }

        public TodoItems Build()
        {
            return new TodoItems
            {
                Title = _title,
                Description = _description,
                Status = _status
            };
        }
    }
}
