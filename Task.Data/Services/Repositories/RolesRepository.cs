using Microsoft.EntityFrameworkCore;
using Tasks.Data.Constants;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.Data.Services.Repositories
{
    public sealed class RolesRepository : RepositoryBase, IRolesRepository
    {
        public RolesRepository(TaskDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task<IEnumerable<RoleEntity>> GetUserRolesAsync(int userId)
        { 
            UserEntity user = await this.context.Users
                .Include(user => user.Roles)
                .Where(user => user.UserId == userId)
                .SingleOrDefaultAsync();

            return user.Roles;
        }

        public async Task<RoleEntity> GetConfirmedUserRoleAsync()
            => await this.context.Roles.FirstOrDefaultAsync(role => role.RoleName == Roles.ConfirmedUser);

        public async Task<RoleEntity> GetNotConfirmedUserRoleAsync()
            => await this.context.Roles.FirstOrDefaultAsync(role => role.RoleName == Roles.NotConfirmedUser);
    }
}
