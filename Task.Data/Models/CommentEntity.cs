using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CommentDate { get; set; }

        public virtual TaskEntity? Task { get; set; }
        public virtual UserEntity? User { get; set; }
    }
}
