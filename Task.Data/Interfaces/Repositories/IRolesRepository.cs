using Tasks.Data.Models;

namespace Tasks.Data.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Task<IEnumerable<RoleEntity>> GetUserRolesAsync(int userId);

        Task<RoleEntity> GetConfirmedUserRoleAsync();

        Task<RoleEntity> GetNotConfirmedUserRoleAsync();
    }
}
