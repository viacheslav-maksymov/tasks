using Tasks.Data.Models;

namespace Tasks.Data.Interfaces
{
    public interface ITaskCategoriesRepository
    {
        Task<TaskCategoryEntity?> GetTaskCategoryAsync(int id);

        Task<IEnumerable<TaskCategoryEntity>> GetCategoriesAsync();
    }
}