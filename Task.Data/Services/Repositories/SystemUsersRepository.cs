using Microsoft.EntityFrameworkCore;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public sealed class SystemUsersRepository : RepositoryBase, ISystemUsersRepository
    {
        public SystemUsersRepository(TaskDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task AddSystemUserAsync(SystemUserEntity user)
        {
            await this.context.SystemUsers.AddAsync(user);

            await this.SaveChangesAsync();
        }

        public async Task<SystemUserEntity?> GetSystemUserAsync(int id)
            => await this.context.SystemUsers.Where(user => user.SystemUserId == id).SingleOrDefaultAsync();

        public async Task<SystemUserEntity?> GetSystemUserAsync(string email)
            => await this.context.SystemUsers.Where(user => user.Email == email).SingleOrDefaultAsync();

        public async Task<bool> IsSystemUserExistAsync(int id)
            => await this.context.SystemUsers.AnyAsync(user => user.SystemUserId == id);

        public async Task<bool> IsEmailExistAsync(string email)
            => await this.context.SystemUsers.AnyAsync(user => user.Email == email);
    }
}
