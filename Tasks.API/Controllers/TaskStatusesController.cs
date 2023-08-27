using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Helpers;
using Tasks.API.Models.TaskStatus;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/taskStatuses")]
    public sealed class TaskStatusesController : ControllerBase
    {
        private readonly ILogger<TaskStatusesController> logger;

        public ITaskStatusesRepository repository { get; }

        private readonly IMapper mapper;

        [HttpPost]
        public async Task<ActionResult<TaskStatusDto>> CreateTaskStatus(TaskStatusCreateDto taskStatus)
            => await this.HandleRequestAsync(async () =>
            {
                TaskStatusEntity statusEntity = this.mapper.Map<TaskStatusEntity>(taskStatus);

                await this.repository.AddTaskStatusAsync(statusEntity);

                TaskStatusDto taskStatusDtoToReturn = this.mapper.Map<TaskStatusDto>(statusEntity);

                return this.CreatedAtRoute("GetTaskStatus",
                    new
                    {
                        id = taskStatusDtoToReturn.StatusId
                    },
                    taskStatusDtoToReturn);
            }, this.logger);

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskStatus(int id)
            => await this.HandleRequestAsync(async () =>
            {
                TaskStatusEntity statusEntity = await this.repository.GetTaskStatusAsync(id);

                if (statusEntity is null)
                    return this.NotFound();

                await this.repository.DeleteTaskStatusAsync(statusEntity);

                return this.NoContent();
            }, this.logger);

        [HttpGet("{id}", Name = "GetTaskStatus")]
        public async Task<ActionResult<TaskStatusDto>> GetTaskStatus(int id)
            => await this.HandleRequestAsync(async () =>
            {
                TaskStatusEntity statusEntity = await this.repository.GetTaskStatusAsync(id);

                if (statusEntity is null)
                    return this.NotFound();

                return this.Ok(this.mapper.Map<TaskStatusDto>(statusEntity));
            }, this.logger);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskStatusDto>>> GetTaskStatuses()
            => await this.HandleRequestAsync(async () =>
            {
                IEnumerable<TaskStatusEntity> statusEntities = await this.repository.GetTaskStatusesAsync();

                return this.Ok(this.mapper.Map<IEnumerable<TaskStatusDto>>(statusEntities));
            }, this.logger);

        public TaskStatusesController(ILogger<TaskStatusesController> logger, 
            ITaskStatusesRepository repository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<TaskStatusDto>> UpdateTaskStatus(int id, TaskStatusUpdateDto taskStatus)
        {
            throw new NotImplementedException();
        }
    }
}
