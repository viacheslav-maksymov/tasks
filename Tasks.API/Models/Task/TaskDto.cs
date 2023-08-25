using Tasks.API.Models.Category;
using Tasks.API.Models.TaskStatus;

namespace Tasks.API.Models.Task
{
    public sealed class TaskDto
    {
        public int TaskId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public int PriorityId { get; set; }

        public int StatusId { get; set; }

        public int ProjectId { get; set; }

        public TaskStatusDto Status { get; set; }

        public TaskCategoryDto Category { get; set; }
    }
}
