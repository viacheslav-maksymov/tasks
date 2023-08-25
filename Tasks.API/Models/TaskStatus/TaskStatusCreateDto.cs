using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.TaskStatus
{
    public sealed class TaskStatusCreateDto
    {
        [Required(ErrorMessage = "You should provide a status name.")]
        [MaxLength(50)]
        public string? StatusName { get; set; }

        [Required]
        public int StatusOrder { get; set; }
    }
}
