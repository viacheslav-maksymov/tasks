using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Data.Models
{
    public partial class AttachmentEntity
    {
        [Key]
        public int AttachmentId { get; set; }
        public int? TaskId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadDate { get; set; }

        public virtual TaskEntity? Task { get; set; }
    }
}
