using Microsoft.AspNetCore.Mvc;
using Tasks.API.Models.TaskStatus;

namespace Tasks.API.Helpers
{
    public static class ControllerBaseRequestExtenstions
    {
        public async static Task<ActionResult> HandleRequestAsync(this ControllerBase controller, Func<Task<ActionResult>> function, ILogger? logger = null)
        {
            try
            {
                return await function();
            }
            catch (Exception e)
            {
                logger?.LogCritical(e.Message, e);
                return controller.StatusCode(500, "A problem happened handling your request.") as ActionResult;
            }
        }
    }
}
