using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Controllers.Authentication;
using Tasks.API.Helpers;
using Tasks.API.Models.User;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.API.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ValdiateUserIdFilter))]
    [Route("api/users")]
    public sealed class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;

        private readonly IUsersRepository repository;

        private readonly ISystemUsersRepository systemUsersRepository;

        private readonly IMapper mapper;

        private readonly IPasswordHashHandler passwordHashHandler;

        public UsersController(ILogger<UsersController> logger,
            IUsersRepository repository,
            ISystemUsersRepository systemUsersRepository,
            IMapper mapper,
            IPasswordHashHandler passwordHashHandler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.systemUsersRepository = systemUsersRepository ?? throw new ArgumentNullException(nameof(systemUsersRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.passwordHashHandler = passwordHashHandler ?? throw new ArgumentNullException(nameof(passwordHashHandler));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto user)
            => await this.HandleRequestAsync(async () =>
            {
                if (await this.systemUsersRepository.IsEmailExistAsync(user.Email))
                    return this.Conflict(new { error = "Email already exists" });

                UserEntity userEntity = this.mapper.Map<UserEntity>(user);
                await this.repository.AddUserAsync(userEntity);

                SystemUserEntity systemUserEntity = this.mapper.Map<SystemUserEntity>(user);
                systemUserEntity.PasswordHash = this.passwordHashHandler.GetPasswordHash(systemUserEntity.PasswordHash);
                systemUserEntity.UserId = userEntity.UserId;
                await this.systemUsersRepository.AddSystemUserAsync(systemUserEntity);

                UserDto userDtoToReturn = this.mapper.Map<UserDto>(userEntity);

                return this.CreatedAtRoute("GetUser",
                    new
                    {
                        id = userDtoToReturn.UserId
                    },
                    userDtoToReturn);
            }, this.logger);


        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUser()
             => await this.HandleRequestAsync(async () =>
             {
                int currentUserId = this.GetClaimUserIdValue();

                UserEntity userEntity = await this.repository.GetUserAsync(currentUserId);

                if (userEntity is null)
                    return this.NotFound();

                return this.Ok(this.mapper.Map<UserDto>(userEntity));
            }, this.logger);
    }
}
