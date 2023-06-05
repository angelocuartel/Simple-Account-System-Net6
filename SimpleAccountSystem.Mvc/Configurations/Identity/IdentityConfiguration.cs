using Microsoft.AspNetCore.Identity;
using SimpleAccountSystem.Mvc.Configurations.Constants;

namespace SimpleAccountSystem.Mvc.Configurations.Identity
{
    public static class IdentityConfiguration
    {

        public static void SetSecuredPasswordPolicy(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = IdentityPasswordConstant.RequireNonAlphanumeric;
                opt.Password.RequiredLength = IdentityPasswordConstant.RequiredLength;
                opt.Password.RequireLowercase = IdentityPasswordConstant.RequiredLowerCase;
                opt.Password.RequiredUniqueChars = IdentityPasswordConstant.RequiredUniqueChars;
                opt.Password.RequireDigit = IdentityPasswordConstant.RequireDigit;
                opt.Password.RequireUppercase = IdentityPasswordConstant.RequireUppercase;
            });
        }
    }
}
