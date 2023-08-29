using System;
using Tasks.Data.Models;

namespace Tasks.Data.Interfaces.Repositories
{
    public interface ITaskStatusesRepository
    {
        Task AddTaskStatusAsync(TaskStatusEntity taskStatus);

        Task DeleteTaskStatusAsync(TaskStatusEntity taskStatus);

        Task<TaskStatusEntity> GetTaskStatusAsync(int id);

        Task<IEnumerable<TaskStatusEntity>> GetTaskStatusesAsync();

        Task<bool> IsTaskStatusExistAsync(int id);
    }
}
