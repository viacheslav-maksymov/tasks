using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Models.Authentication
{
    public sealed class AuthenticationRequestBody
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
