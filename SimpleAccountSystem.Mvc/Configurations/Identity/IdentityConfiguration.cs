﻿using Microsoft.AspNetCore.Identity;
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

                opt.Lockout.MaxFailedAccessAttempts = IdentityLockConfigConstant.AttemptsBeforeLockout;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });
        }

        public static void SetIdentitySessionCookiesConfiguration(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.IsEssential = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opt.Cookie.HttpOnly = false;
            });
        }
    }
}
