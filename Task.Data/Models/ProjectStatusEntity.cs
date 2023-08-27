using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            this.Projects = new HashSet<ProjectEntity>();
        }

        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public int StatusOrder { get; set; }

        public virtual ICollection<ProjectEntity> Projects { get; set; }
    }
}
