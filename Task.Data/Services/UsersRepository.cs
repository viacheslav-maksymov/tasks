using Microsoft.EntityFrameworkCore;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.Data.Services
{
    public sealed class UsersRepository : RepositoryBase, IUsersRepository
    {
        public UsersRepository(TaskDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await this.context.Users.AddAsync(user);

            await this.SaveChangesAsync();
        }

        public async Task<UserEntity?> GetUserAsync(int id)
            => await this.context.Users.Where(user => user.UserId == id).SingleOrDefaultAsync();

        public async Task<UserEntity?> GetUserAsync(string userName)
            => await this.context.Users.Where(user => user.UserName == userName).SingleOrDefaultAsync();

        public async Task<bool> IsUserExistAsync(int id)
            => await this.context.Users.AnyAsync(user => user.UserId == id);

        public async Task<bool> IsUserExistAsync(string userName)
            => await this.context.Users.AnyAsync(user => user.UserName == userName);
    }
}
