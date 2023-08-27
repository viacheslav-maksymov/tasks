using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tasks.API.Constants;
using Tasks.Data.Interfaces;

namespace Tasks.API.Controllers.Authentication
{
    public sealed class ValdiateUserIdFilter : IAsyncAuthorizationFilter
    {
        private readonly IUsersRepository repository;

        public ValdiateUserIdFilter(IUsersRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .Any(metadata => metadata.GetType() == typeof(AllowAnonymousAttribute));

            if (hasAllowAnonymous)
                return;

            var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == TokenClaims.IdClaim);
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            int userId = int.Parse(userIdClaim.Value);

            if (!await this.repository.IsUserExistAsync(userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
