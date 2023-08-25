using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class UserSettingEntity
    {
        [Key]
        public int SettingId { get; set; }
        public int? UserId { get; set; }
        public string SettingName { get; set; } = null!;
        public string SettingValue { get; set; } = null!;

        public virtual UserEntity? User { get; set; }
    }
}
