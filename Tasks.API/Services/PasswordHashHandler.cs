using Org.BouncyCastle.Crypto.Generators;
using System.Text;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces;

namespace Tasks.API.Services
{
    public sealed class PasswordHashHandler : IPasswordHashHandler
    {
        private readonly IConfiguration configuration;

        public string GetPasswordHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public PasswordHashHandler(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public bool VerifyPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
