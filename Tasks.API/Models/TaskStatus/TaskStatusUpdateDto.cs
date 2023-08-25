using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.TaskStatus
{
    public sealed class TaskStatusUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string? StatusName { get; set; }

        [Required]
        public int StatusOrder { get; set; }
    }
}
