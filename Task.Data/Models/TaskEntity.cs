using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class TaskEntity
    {
        public TaskEntity()
        {
            this.Attachments = new HashSet<AttachmentEntity>();
            this.Comments = new HashSet<CommentEntity>();
            this.DependentTasks = new HashSet<TaskEntity>();
            this.Tags = new HashSet<TagEntity>();
            this.Tasks = new HashSet<TaskEntity>();
        }

        [Key]
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? Date { get; set; }
        public int? CategoryId { get; set; }
        public int? PriorityId { get; set; }
        public int? StatusId { get; set; }
        public int? ProjectId { get; set; }

        public virtual TaskCategoryEntity? Category { get; set; }
        public virtual TaskPriorityEntity? Priority { get; set; }
        public virtual ProjectEntity? Project { get; set; }
        public virtual TaskStatusEntity? Status { get; set; }
        public virtual UserEntity? User { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }

        public virtual ICollection<TaskEntity> DependentTasks { get; set; }
        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
