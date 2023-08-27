using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tasks.API.Controllers.Authentication;
using Tasks.API.Helpers;
using Tasks.API.Models.Task;
using Tasks.API.Models.TaskStatus;
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
    public sealed class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;

        private readonly ITasksRepository repository;

        private readonly IMapper mapper;

        private readonly IUsersRepository userRepository;

        private readonly ITaskStatusesRepository taskStatusesRepository;

        private readonly ITaskCategoriesRepository taskCategoriesRepository;

        private readonly ILogDatabaseRepository logRepository;

        public TasksController(ILogger<TasksController> logger,
            ITasksRepository repository, 
            IUsersRepository userRepository,
            ITaskStatusesRepository taskStatusesRepository,
            ITaskCategoriesRepository taskCategoriesRepository,
            IMapper mapper,
            ILogDatabaseRepository logRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.taskStatusesRepository = taskStatusesRepository ?? throw new ArgumentNullException(nameof(taskStatusesRepository));
            this.taskCategoriesRepository = taskCategoriesRepository ?? throw new ArgumentNullException(nameof(taskCategoriesRepository));
            this.logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        [HttpPost("tasks")]
        public async Task<ActionResult<TaskStatusDto>> CreateTask(TaskCreateDto task)
            => await this.HandleRequestAsync(async () =>
            {
                int currentUserId = this.GetClaimUserIdValue();
                if (currentUserId != task.UserId)
                    return this.Forbid();

                if (task.StatusId != null && !await this.taskStatusesRepository.IsTaskStatusExistAsync((int)task.StatusId))
                    return this.NotFound($"The {nameof(task.StatusId)}: {task.StatusId} doesn't exist.");

                if (task.CategoryId != null && !await this.taskCategoriesRepository.IsTaskCategoryExistAsync((int)task.CategoryId))
                    return this.NotFound($"The {nameof(task.CategoryId)}: {task.CategoryId} doesn't exist.");

                TaskEntity taskEntity = this.mapper.Map<TaskEntity>(task);
                taskEntity.Date = DateTime.Now;

                await this.repository.AddTaskAsync(taskEntity);

                TaskDto taskDtoToReturn = this.mapper.Map<TaskDto>(taskEntity);

                return this.CreatedAtRoute("GetTask",
                    new
                    {
                        id = taskDtoToReturn.TaskId
                    },
                    taskDtoToReturn);
            }, this.logger);

        [HttpDelete("tasks/{id}")]
        public async Task<ActionResult> DeleteTask(int id)
            => await this.HandleRequestAsync(async () =>
            {
                TaskEntity taskEntity = await this.repository.GetTaskAsync(id);

                if (taskEntity is null)
                    return this.NotFound();

                int currentUserId = this.GetClaimUserIdValue();
                if (currentUserId != taskEntity.UserId)
                    return this.Forbid();

                await this.repository.DeleteTaskAsync(taskEntity);

                return this.NoContent();
            }, this.logger);

        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
            => await this.HandleRequestAsync(async () =>
            {
                int currentUserId = this.GetClaimUserIdValue();

                IEnumerable<TaskEntity> taskEntitites = await this.repository.GetTasksByUserAsync(currentUserId);

                return this.Ok(this.mapper.Map<IEnumerable<TaskDto>>(taskEntitites));
            }, this.logger);

        [HttpGet("tasks/{id}", Name = "GetTask")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
            => await this.HandleRequestAsync(async () =>
            {
                TaskEntity? taskEntity = await this.repository.GetTaskAsync(id);

                if (taskEntity is null)
                    return this.NotFound();

                int currentUserId = this.GetClaimUserIdValue();
                if (currentUserId != taskEntity.UserId)
                    return this.Forbid();

                return this.Ok(this.mapper.Map<TaskDto>(taskEntity));
            }, this.logger);

        [HttpGet("taskStatus/{statusId}/tasks")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(int statusId)
            => await this.HandleRequestAsync(async () =>
            {
                int currentUserId = this.GetClaimUserIdValue();

                IEnumerable<TaskEntity> taskEntitites = await this.repository.GetTasksByUserAsync(currentUserId);

                IEnumerable<TaskEntity> taskWithStatus = taskEntitites.Where(task => task.StatusId == statusId);

                if (!taskWithStatus.Any())
                    return this.NotFound();

                return this.Ok(this.mapper.Map<IEnumerable<TaskDto>>(taskWithStatus));
            }, this.logger);

        [HttpPut("tasks/{id}")]
        public async Task<ActionResult> UpdateTask(int id, TaskUpdateDto task)
            => await this.HandleRequestAsync(async () =>
            {
            if (!await this.repository.IsTaskExistAsync(id))
                return this.NotFound();

            TaskEntity taskEntity = await this.repository.GetTaskAsync(id);

            int currentUserId = this.GetClaimUserIdValue();
            if (currentUserId != taskEntity.UserId)
                return this.Forbid();

            taskEntity.Title = task.Title;
            taskEntity.Description = task.Description;
            taskEntity.CategoryId = task.CategoryId;
            taskEntity.PriorityId = task.PriorityId;
            taskEntity.ProjectId = task.ProjectId;
            taskEntity.StatusId = task.StatusId;

            await this.repository.UpdateTaskAsync(taskEntity);

            TaskUpdateLog log = this.mapper.Map<TaskUpdateLog>(taskEntity);
            if (!await this.logRepository.FileUpdateTaskLogAsync(log))
                this.logger.LogError($"Task update log not saved.\n{taskEntity.TaskId}");

            return this.NoContent();
        }, this.logger);
    }
}
