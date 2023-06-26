namespace SimpleAccountSystem.Mvc.Configurations.Session
{
    public static class SessionConfiguration
    {
        public static void AddSessionWithDefaultConfiguration(this IServiceCollection service)
        {
            service.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = false;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opt.Cookie.Path = "/";
                opt.Cookie.Name = "_MainSession";
                opt.IdleTimeout = TimeSpan.FromMinutes(15);
                opt.Cookie.IsEssential = true;
            });
        }
    }
}
