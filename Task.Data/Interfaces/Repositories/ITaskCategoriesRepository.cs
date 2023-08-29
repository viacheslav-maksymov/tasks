using Tasks.Data.Models;

namespace Tasks.Data.Interfaces.Repositories
{
    public interface ITaskCategoriesRepository
    {
        Task<TaskCategoryEntity?> GetTaskCategoryAsync(int id);

        Task<IEnumerable<TaskCategoryEntity>> GetCategoriesAsync();

        Task<bool> IsTaskCategoryExistAsync(int id);
    }
}