using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class RoleEntity
    {
        public RoleEntity()
        {
            this.Users = new HashSet<UserEntity>();
        }

        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
