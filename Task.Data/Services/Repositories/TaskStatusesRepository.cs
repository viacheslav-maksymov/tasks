using Microsoft.EntityFrameworkCore;
using System;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public sealed class TaskStatusesRepository : RepositoryBase, ITaskStatusesRepository
    {
        public TaskStatusesRepository(TaskDatabaseContext databaseContext) 
            : base(databaseContext)
        {
        }

        public async Task AddTaskStatusAsync(TaskStatusEntity taskStatus)
        {
            await this.context.TaskStatuses.AddAsync(taskStatus);

            await this.SaveChangesAsync();
        }

        public async Task DeleteTaskStatusAsync(TaskStatusEntity taskStatus)
        {
            this.context.TaskStatuses.Remove(taskStatus);

            await this.SaveChangesAsync();
        }

        public async Task<TaskStatusEntity?> GetTaskStatusAsync(int id)
            => await this.context.TaskStatuses.Where(status => status.StatusId == id).SingleOrDefaultAsync();

        public async Task<IEnumerable<TaskStatusEntity>> GetTaskStatusesAsync()
            => await this.context.TaskStatuses.OrderBy(status => status.StatusOrder).ToListAsync();

        public async Task<TaskStatusEntity> GetTaskStatusesByTaskAsync(int taskId)
        { 
            var task = await this.context.Tasks.Include(task => task.Status).FirstOrDefaultAsync(task => task.TaskId == taskId);

            return task.Status;
        }

        public async Task<bool> IsTaskStatusExistAsync(int id)
            => await this.context.TaskStatuses.AnyAsync(status => status.StatusId == id);
    }
}
