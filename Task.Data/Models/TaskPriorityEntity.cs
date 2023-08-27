using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TaskPriority
    {
        public TaskPriority()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int PriorityId { get; set; }
        public string PriorityName { get; set; } = null!;

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
