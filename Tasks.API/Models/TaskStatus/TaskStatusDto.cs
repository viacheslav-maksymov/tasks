namespace Tasks.API.Models.TaskStatus
{
    public sealed class TaskStatusDto
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public int StatusOrder { get; set; }
    }
}
