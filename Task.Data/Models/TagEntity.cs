using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TagEntity
    {
        public TagEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
