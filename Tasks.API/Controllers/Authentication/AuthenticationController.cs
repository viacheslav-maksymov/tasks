using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Models.Authentication;
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

        private readonly IPasswordHashHandler passwordHashHandler;

        private readonly ITokenManager tokenManager;

        public AuthenticationController(IUsersRepository repository,
            IPasswordHashHandler passwordHashHandler,
            ITokenManager tokenManager)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.passwordHashHandler = passwordHashHandler ?? throw new ArgumentNullException(nameof(passwordHashHandler));
            this.tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody requestBody)
        {
            if (!await this.repository.IsUserExistAsync(requestBody.UserName))
                return this.NotFound();

            UserEntity user = await this.repository.GetUserAsync(requestBody.UserName);

            if (!this.ValidateUserCredentials(user, requestBody.Password))
                return this.Unauthorized();

            string token = this.tokenManager.GetToken(user.UserId.ToString(), user.Email);

            return this.Ok(new { token = token });
        }

        private bool ValidateUserCredentials(UserEntity user, string? password)
            => this.passwordHashHandler.VerifyPassword(password, user?.PasswordHash);
    }
}
