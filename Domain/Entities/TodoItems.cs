using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo_API2.Domain.Enums;

namespace Todo_API2.Domain.Entities
{
    public class TodoItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public TodoItemStatus Status { get; set; } = TodoItemStatus.NotStarted;

        public TodoItems()
        {
            Status = TodoItemStatus.NotStarted;
        }
    }
}
