using System;
using System.Text.Json.Serialization;

namespace Tasks.Log.Models
{
    public partial class TaskUpdateLog
    {
        [JsonPropertyName("guid")]
        public string? Guid { get; set; }

        [JsonPropertyName("task_id")]
        public int? TaskId { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("priority_id")]
        public int? PriorityId { get; set; }

        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }

        [JsonPropertyName("project_id")]
        public int? ProjectId { get; set; }

        [JsonPropertyName("category_name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("priority_name")]
        public string? PriorityName { get; set; }

        [JsonPropertyName("project_name")]
        public string? ProjectName { get; set; }

        [JsonPropertyName("status_name")]
        public string? StatusName { get; set; }

        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("attachment_list")]
        public string? AttachmentList { get; set; }

        [JsonPropertyName("comment_list")]
        public string? CommentList { get; set; }

        [JsonPropertyName("update_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
