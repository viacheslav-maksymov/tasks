using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class ProjectStatusEntity
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public int StatusOrder { get; set; }
    }
}
