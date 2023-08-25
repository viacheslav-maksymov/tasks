using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tasks.API.Helpers;
using Tasks.API.Models.User;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public sealed class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;

        private readonly IUsersRepository repository;

        private readonly IMapper mapper;

        private readonly IPasswordHashHandler passwordHashHandler;

        public UsersController(ILogger<UsersController> logger,
            IUsersRepository repository,
            IMapper mapper,
            IPasswordHashHandler passwordHashHandler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(logger));
            this.passwordHashHandler = passwordHashHandler ?? throw new ArgumentNullException(nameof(passwordHashHandler));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto user)
            => await this.HandleRequestAsync(async () =>
            {
                UserEntity userEntity = this.mapper.Map<UserEntity>(user);

                userEntity.PasswordHash = this.passwordHashHandler.GetPasswordHash(user.Password);

                await this.repository.AddUserAsync(userEntity);

                UserDto userDtoToReturn = this.mapper.Map<UserDto>(userEntity);

                return this.CreatedAtRoute("GetUser",
                    new
                    {
                        id = userDtoToReturn.UserId
                    },
                    userDtoToReturn);
            }, this.logger);

        [Authorize]
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            int currentUserId = int.Parse(this.User.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value);

            if (currentUserId != id)
                return this.Forbid();

            UserEntity userEntity = await this.repository.GetUserAsync(id);

            if (userEntity is null)
                return this.NotFound();

            return this.Ok(this.mapper.Map<UserDto>(userEntity));
        }

        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            int currentUserId = int.Parse(this.User.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value);

            UserEntity userEntity = await this.repository.GetUserAsync(currentUserId);

            if (userEntity is null)
                return this.NotFound();

            return this.Ok(this.mapper.Map<UserDto>(userEntity));
        }
    }
}
