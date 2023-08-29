using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class UserEntity
    {
        public UserEntity()
        {
            this.Comments = new HashSet<CommentEntity>();
            this.SystemUsers = new HashSet<SystemUserEntity>();
            this.Tasks = new HashSet<TaskEntity>();
            this.UserSettings = new HashSet<UserSettingEntity>();
            this.Projects = new HashSet<ProjectEntity>();
            this.Roles = new HashSet<RoleEntity>();
        }

        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsEmailConfirmed { get; set; }

        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<SystemUserEntity> SystemUsers { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<UserSettingEntity> UserSettings { get; set; }

        public virtual ICollection<ProjectEntity> Projects { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; }
    }
}
