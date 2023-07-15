using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SimpleAccountSystem.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StepUpAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        public const string stepUpPath = "stepUpPath";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user != null)
            {
                var name = user.FindFirst(ClaimTypes.Name)?.Value;
                var requestPath = context.HttpContext.Session.GetString(name + stepUpPath);

                if (!string.IsNullOrEmpty(requestPath) && context.HttpContext.Request.Path.Equals(requestPath))
                {
                    context.HttpContext.Session.Remove(name + stepUpPath);
                    return;

                }

                context.HttpContext.Response.Redirect("/Identity/Account/LoginWith2fa?returnUrl=" + context.HttpContext.Request.Path);
                return;
            }

            context.HttpContext.Response.Redirect("/Identity/Account/Login");
        }
    }
}
