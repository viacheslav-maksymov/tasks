using Microsoft.EntityFrameworkCore;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public sealed class TasksRepository : RepositoryBase, ITasksRepository
    {
        public TasksRepository(TaskDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task AddTaskAsync(TaskEntity task)
        {
            await this.context.Tasks.AddAsync(task);

            await this.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskEntity task)
        {
            this.context.Tasks.Remove(task);

            await this.SaveChangesAsync();
        }

        public async Task<TaskEntity?> GetTaskAsync(int id)
            => await this.context.Tasks
            .Where(task => task.TaskId == id)
            .Include(task => task.Status)
            .Include(task => task.Category)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<TaskEntity>> GetTasksAsync()
            => await this.context.Tasks
            .Include(task => task.Status)
            .Include(task => task.Category)
            .OrderBy(status => status.Status.StatusOrder)
            .ToListAsync();

        public async Task<IEnumerable<TaskEntity>> GetTasksByUserAsync(int userId)
            => await this.context.Tasks
            .Where(task => task.UserId == userId)
            .Include(task => task.User)
            .Include(task => task.Status)
            .Include(task => task.Category)
            .ToListAsync();

        public async Task<bool> IsTaskExistAsync(int id)
            => await this.context.Tasks.AnyAsync(task => task.TaskId == id);

        public async Task UpdateTaskAsync(TaskEntity taskEntity)
        {
            this.context.Tasks.Update(taskEntity);
            await this.context.SaveChangesAsync();
        }
    }
}
