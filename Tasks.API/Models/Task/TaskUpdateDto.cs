using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.Task
{
    public sealed class TaskUpdateDto
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public int? PriorityId { get; set; }

        public int? StatusId { get; set; }

        public int? ProjectId { get; set; }
    }
}