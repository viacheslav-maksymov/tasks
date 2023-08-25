using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TaskStatusEntity
    {
        public TaskStatusEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public int StatusOrder { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
