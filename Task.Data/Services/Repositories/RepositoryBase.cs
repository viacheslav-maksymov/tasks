using System;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly TaskDatabaseContext context;

        public RepositoryBase(TaskDatabaseContext databaseContext)
        {
            this.context = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        protected async Task<bool> SaveChangesAsync()
            => await this.context.SaveChangesAsync() >= 0;
    }
}
