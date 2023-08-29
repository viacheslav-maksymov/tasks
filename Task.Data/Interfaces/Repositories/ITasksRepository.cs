using Tasks.Data.Models;

namespace Tasks.Data.Interfaces.Repositories
{
    public interface ITasksRepository
    {
        Task AddTaskAsync(TaskEntity task);

        Task DeleteTaskAsync(TaskEntity task);

        Task<TaskEntity?> GetTaskAsync(int id);

        Task<IEnumerable<TaskEntity>> GetTasksAsync();

        Task<IEnumerable<TaskEntity>> GetTasksByUserAsync(int userId);

        Task<bool> IsTaskExistAsync(int id);

        Task UpdateTaskAsync(TaskEntity taskEntity);
    }
}