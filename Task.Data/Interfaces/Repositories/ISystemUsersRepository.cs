using Tasks.Data.Models;

namespace Tasks.Data.Interfaces.Repositories
{
    public interface ISystemUsersRepository
    {
        Task AddSystemUserAsync(SystemUserEntity user);

        Task<SystemUserEntity?> GetSystemUserAsync(int id);

        Task<SystemUserEntity?> GetSystemUserAsync(string email);

        Task<bool> IsSystemUserExistAsync(int id);

        Task<bool> IsEmailExistAsync(string email);
    }
}
