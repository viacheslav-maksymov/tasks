using Microsoft.AspNetCore.Mvc;
using Tasks.API.Constants;

namespace Tasks.API.Helpers
{
    public static class ControllerBaseRequestExtenstions
    {
        public static int GetClaimUserIdValue(this ControllerBase controller)
            => int.Parse(controller.User.Claims.FirstOrDefault(claim => claim.Type == TokenClaims.IdClaim)?.Value);

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
