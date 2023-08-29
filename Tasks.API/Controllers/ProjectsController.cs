using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Controllers.Authentication;
using Tasks.Data.Constants;
using Tasks.Data.Interfaces.Repositories;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ConfirmedUser)]
    [TypeFilter(typeof(ValdiateUserIdFilter))]
    [Route("api/users")]
    public sealed class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IUsersRepository repository;
        private readonly IMapper mapper;

        public ProjectsController(ILogger<ProjectsController> logger,
            IUsersRepository repository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
