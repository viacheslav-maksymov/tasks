using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Tasks.Log.Interfaces;
using Tasks.Log.Models;

namespace Tasks.Log.Data
{
    public sealed class MongoDbDatabase : ILogDatabaseRepository
    {
        private const string collectionName = "logs";

        private readonly MongoDbSettings mongoDbSettings;

        public MongoDbDatabase(IOptions<MongoDbSettings> mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings.Value;
        }

        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            return collection;
        }

        private TaskUpdateLog ConvertBsonToTaskUpdateLog(BsonDocument document)
        {
            if (document is null)
                return null;


            return new TaskUpdateLog
            {
                Guid = document["guid"].AsString,
                TaskId = document["task_id"].AsInt32,
                UserId = document["user_id"].AsInt32,
                Title = document["title"].AsString,
                Description = document["description"].AsString,
                CategoryId = document["category_id"].AsInt32,
                PriorityId = document["priority_id"].AsInt32,
                StatusId = document["status_id"].AsInt32,
                ProjectId = document["project_id"].AsInt32,
                CategoryName = document["category_name"].AsString,
                PriorityName = document["priority_name"].AsString,
                ProjectName = document["project_name"].AsString,
                StatusName = document["status_name"].AsString,
                UserName = document["user_name"].AsString,
                AttachmentList = document["attachment_list"].AsString,
                CommentList = document["comment_list"].AsString,
                UpdatedDate = document["udpate_date"].AsBsonDateTime.ToLocalTime()
            };
        }

        public async Task<List<TaskUpdateLog>> GetAllTaskUpdateLogsAsync()
        {
            var collection = this.GetCollection(MongoDbDatabase.collectionName);
            var documents = collection.Find(_ => true).ToListAsync();

            var logs = new List<TaskUpdateLog>();

            if (documents is null)
                return logs;

            foreach (var document in await documents)
                logs.Add(this.ConvertBsonToTaskUpdateLog(document));

            return logs;
        }

        public async Task<List<TaskUpdateLog>> GetTaskUpdateLogsByTaskAsync(int taskId)
        {
            var collection = this.GetCollection(MongoDbDatabase.collectionName);
            var documents = collection.Find(
                Builders<BsonDocument>.Filter.Eq("task_id", taskId)).ToListAsync();

            var logs = new List<TaskUpdateLog>();

            if (documents is null)
                return logs;

            foreach (var document in await documents)
                logs.Add(this.ConvertBsonToTaskUpdateLog(document));

            return logs;
        }

        public async Task<bool> FileUpdateTaskLogAsync(TaskUpdateLog taskUpdateLog)
        {
            var collection = this.GetCollection(MongoDbDatabase.collectionName);

            var document = new BsonDocument
            {
                { "guid", Guid.NewGuid().ToString("N")},
                { "task_id", taskUpdateLog.TaskId ?? 0},
                { "user_id", taskUpdateLog.UserId ?? 0},
                { "title", taskUpdateLog.Title ?? string.Empty},
                { "description", taskUpdateLog.Description ?? string.Empty},
                { "category_id", taskUpdateLog.CategoryId ?? 0},
                { "priority_id", taskUpdateLog.PriorityId ?? 0},
                { "status_id", taskUpdateLog.StatusId ?? 0},
                { "project_id", taskUpdateLog.ProjectId ?? 0},
                { "category_name", taskUpdateLog.CategoryName ?? string.Empty},
                { "priority_name", taskUpdateLog.PriorityName ?? string.Empty},
                { "project_name", taskUpdateLog.ProjectName ?? string.Empty},
                { "status_name", taskUpdateLog.StatusName ?? string.Empty},
                { "user_name", taskUpdateLog.UserName ?? string.Empty},
                { "attachment_list", taskUpdateLog.AttachmentList ?? string.Empty},
                { "comment_list", taskUpdateLog.CommentList ?? string.Empty},
                { "udpate_date", DateTime.Now }
            };

            try
            {
                await collection.InsertOneAsync(document);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUpdateTaskLogAsync(string guid)
        {
            var collection = this.GetCollection(MongoDbDatabase.collectionName);

            var result = await collection.DeleteOneAsync(
                Builders<BsonDocument>.Filter.Eq("guid", guid));

            return result.DeletedCount > 0;
        }
    }
}
