using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.User
{
    public sealed class UserCreateDto
    {
        [Required(ErrorMessage = "You should provide a username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You should provide an email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should provide a password.")]
        public string Password { get; set; }
    }
}
