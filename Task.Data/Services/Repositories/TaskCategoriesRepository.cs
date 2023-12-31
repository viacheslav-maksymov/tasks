﻿using Microsoft.EntityFrameworkCore;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public sealed class TaskCategoriesRepository : RepositoryBase, ITaskCategoriesRepository
    {
        public TaskCategoriesRepository(TaskDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task<TaskCategoryEntity?> GetTaskCategoryAsync(int id)
            => await this.context.TaskCategories.Where(task => task.CategoryId == id).SingleOrDefaultAsync();

        public async Task<IEnumerable<TaskCategoryEntity>> GetCategoriesAsync()
            => await this.context.TaskCategories
            .ToListAsync();

        public async Task<bool> IsTaskCategoryExistAsync(int id)
            => await this.context.TaskCategories.AnyAsync(status => status.CategoryId == id);
    }
}
