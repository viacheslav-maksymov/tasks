using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TaskCategoryEntity
    {
        public TaskCategoryEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
