using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TaskPriorityEntity
    {
        public TaskPriorityEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int PriorityId { get; set; }
        public string PriorityName { get; set; } = null!;

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
