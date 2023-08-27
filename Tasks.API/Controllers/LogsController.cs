using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Controllers.Authentication;
using Tasks.API.Helpers;
using Tasks.API.Models.Logs;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;
using Tasks.Log.Interfaces;
using Tasks.Log.Models;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(ValdiateUserIdFilter))]
    [Route("api")]
    public sealed class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> logger;

        private readonly ILogDatabaseRepository repository;

        private readonly IMapper mapper;

        private readonly ITasksRepository taskRepository;

        public LogsController(ILogger<LogsController> logger,
            ILogDatabaseRepository repository,
            ITasksRepository taskRepository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        [HttpGet("tasks/{taskId}/logs")]
        public async Task<ActionResult<IEnumerable<TaskUpdateLogDto>>> GetTaskUpdateLogs(int taskId)
            => await this.HandleRequestAsync(async () =>
            {
                if (!await this.taskRepository.IsTaskExistAsync(taskId))
                    return this.NotFound();

                int currentUserId = this.GetClaimUserIdValue();
                TaskEntity task = await this.taskRepository.GetTaskAsync(taskId);

                if (task.UserId != currentUserId)
                    return this.Forbid();

                List<TaskUpdateLog> logs = await this.repository.GetTaskUpdateLogsByTaskAsync(taskId);

                return this.Ok(this.mapper.Map<IEnumerable<TaskUpdateLogDto>>(logs));
            }, this.logger);
    }
}
