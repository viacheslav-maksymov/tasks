using System;
using Tasks.Data.Models;

namespace Tasks.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task AddUserAsync(UserEntity user);

        Task<UserEntity?> GetUserAsync(int id);

        Task<UserEntity?> GetUserAsync(string userName);

        Task<bool> IsUserExistAsync(int id);

        Task<bool> IsUserExistAsync(string userName);
    }
}
