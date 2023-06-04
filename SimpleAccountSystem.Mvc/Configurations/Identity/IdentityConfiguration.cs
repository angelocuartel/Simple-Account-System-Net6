using Microsoft.AspNetCore.Identity;
using SimpleAccountSystem.Mvc.Constants.Identity;

namespace SimpleAccountSystem.Mvc.Configurations.Identity
{
    public static class IdentityConfiguration
    {

        public static void SetSecuredPasswordPolicy(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = PasswordConstant.RequireNonAlphanumeric;
                opt.Password.RequiredLength = PasswordConstant.RequiredLength;
                opt.Password.RequireLowercase = PasswordConstant.RequiredLowerCase;
                opt.Password.RequiredUniqueChars = PasswordConstant.RequiredUniqueChars;
                opt.Password.RequireDigit = PasswordConstant.RequireDigit;
                opt.Password.RequireUppercase = PasswordConstant.RequireUppercase;
            });
        }
    }
}
