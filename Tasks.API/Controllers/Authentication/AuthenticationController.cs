using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.API.Controllers.Authentication
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsersRepository repository;

        private readonly IConfiguration configuration;

        private readonly IPasswordHashHandler passwordHashHandler;

        public AuthenticationController(IUsersRepository repository, IConfiguration configuration, IPasswordHashHandler passwordHashHandler)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.passwordHashHandler = passwordHashHandler ?? throw new ArgumentNullException(nameof(passwordHashHandler));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody requestBody)
        {
            if (!await this.repository.IsUserExistAsync(requestBody.UserName))
                return this.NotFound();

            UserEntity user = await this.repository.GetUserAsync(requestBody.UserName);

            if (!await this.ValidateUserCredentials(user, requestBody.Password))
                return this.Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["Authentication:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("id", user.UserId.ToString()),
                new Claim("userName", requestBody.UserName),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                this.configuration["Authentication:Issuer"],
                this.configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                signingCredentials);

            string token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return this.Ok(new { token = token });
        }

        private async Task<bool> ValidateUserCredentials(UserEntity user, string? password)
            => this.passwordHashHandler.VerifyPassword(password, user?.PasswordHash);
    }
}
