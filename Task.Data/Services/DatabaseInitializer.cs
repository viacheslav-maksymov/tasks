using Microsoft.EntityFrameworkCore;
using Tasks.Data.Constants;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.Data.Services
{
    public sealed class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly TaskDatabaseContext context;

        public DatabaseInitializer(TaskDatabaseContext databaseContext)
        {
            this.context = databaseContext;
        }

        public async Task InitializeAsync()
        {
            if (!await this.context.Roles.AnyAsync())
            {
                this.context.Roles.Add(new RoleEntity { RoleName = Roles.NotConfirmedUser });
                this.context.Roles.Add(new RoleEntity { RoleName = Roles.ConfirmedUser });
                await this.context.SaveChangesAsync();
            }
        }
    }
}
