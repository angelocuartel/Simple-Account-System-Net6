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
                var isValidStepUpAuth = context.HttpContext.Session.GetInt32("validStepUpAuth") == 1;

                if (isValidStepUpAuth)
                {
                    context.HttpContext.Session.Remove("validStepUpAuth");
                    return;

                }

                context.HttpContext.Response.Redirect("/Identity/Account/LoginWith2fa?returnUrl=" + context.HttpContext.Request.Path);
                return;
            }

            context.HttpContext.Response.Redirect("/Identity/Account/Login");
        }
    }
}
