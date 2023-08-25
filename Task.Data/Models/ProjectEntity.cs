using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class ProjectEntity
    {
        public ProjectEntity()
        {
            this.Tasks = new HashSet<TaskEntity>();
            this.Users = new HashSet<UserEntity>();
        }

        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
