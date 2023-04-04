using System.Text.Json.Serialization;
using Todo_API2.Domain.Enums;

namespace Todo_API2.Presentation.Dtos
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoItemStatus Status { get; set; }
    }
}
