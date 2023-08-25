using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Helpers;
using Tasks.API.Models.Category;
using Tasks.API.Models.Task;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/taskCategories")]
    public sealed class TaskCategoriesController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;

        private readonly ITaskCategoriesRepository repository;

        private readonly IMapper mapper;

        public TaskCategoriesController(ILogger<TasksController> logger,
            ITaskCategoriesRepository repository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TaskCategoryDto>>> GetTasks()
            => await this.HandleRequestAsync(async () =>
            {
                IEnumerable<TaskCategoryEntity> taskCategoryEntities = await this.repository.GetCategoriesAsync();

                return this.Ok(this.mapper.Map<IEnumerable<TaskCategoryDto>>(taskCategoryEntities));
            });

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskCategoryDto>> GetTask(int id)
            => await this.HandleRequestAsync(async () =>
            {
                TaskCategoryEntity? taskCategoryEntity = await this.repository.GetTaskCategoryAsync(id);

                if (taskCategoryEntity is null)
                    return this.NotFound();

                return this.Ok(this.mapper.Map<TaskCategoryDto>(taskCategoryEntity));
            });

    }
}
