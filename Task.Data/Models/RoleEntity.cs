using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            this.Users = new HashSet<UserEntity>();
        }

        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
