using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.Task
{
    public sealed class TaskCreateDto
    {
        [Required(ErrorMessage = "You should provide a user ID.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "You should provide a title.")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "You should provide a description.")]
        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public int? PriorityId { get; set; }

        public int? StatusId { get; set; }

        public int? ProjectId { get; set; }
    }
}
