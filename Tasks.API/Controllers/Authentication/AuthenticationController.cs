using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.API.Helpers;
using Tasks.API.Models.Authentication;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces.Repositories;
using Tasks.Data.Models;

namespace Tasks.API.Controllers.Authentication
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISystemUsersRepository repository;

        private readonly IRolesRepository rolesRepository;

        private readonly IPasswordHashHandler passwordHashHandler;

        private readonly ITokenManager tokenManager;

        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(ISystemUsersRepository repository,
            IRolesRepository rolesRepository,
            IPasswordHashHandler passwordHashHandler,
            ITokenManager tokenManager,
            ILogger<AuthenticationController> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.rolesRepository = rolesRepository ?? throw new ArgumentNullException(nameof(rolesRepository));
            this.passwordHashHandler = passwordHashHandler ?? throw new ArgumentNullException(nameof(passwordHashHandler));
            this.tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody requestBody)
            => await this.HandleRequestAsync(async () =>
            {
                SystemUserEntity systemUser = await this.repository.GetSystemUserAsync(requestBody.Email);

                if (systemUser == null)
                    return this.NotFound();

                if (!this.ValidateUserCredentials(systemUser, requestBody.Password))
                    return this.Unauthorized();

                IEnumerable<string> roles = (await this.rolesRepository
                    .GetUserRolesAsync((int)systemUser.UserId))
                    .Select(role => role.RoleName);

                string token = this.tokenManager.GetToken(systemUser.UserId.ToString(), systemUser.Email, roles);

                return this.Ok(new { token = token });
            }, this.logger);


        private bool ValidateUserCredentials(SystemUserEntity systemUser, string? password)
            => this.passwordHashHandler.VerifyPassword(password, systemUser?.PasswordHash);
    }
}
