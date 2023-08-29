using Tasks.API.Models.Roles;

namespace Tasks.API.Models.User
{
    public sealed class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
