using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class SystemUserEntity
    {
        [Key]
        public int SystemUserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int? UserId { get; set; }

        public virtual UserEntity? User { get; set; }
    }
}
