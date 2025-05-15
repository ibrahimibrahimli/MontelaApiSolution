using Microsoft.AspNetCore.Mvc.Filters;

namespace MontelaApi.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;
            if(!string.IsNullOrEmpty(name) )
            {

            }
        }
    }
}
