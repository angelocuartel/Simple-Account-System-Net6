using SimpleAccountSystem.Domain.Service;

namespace SimpleAccountSystem.Mvc.Configurations.Dependencies
{
    public static class DependencyConfiguration
    {
        public static void InjectDependencies(this IServiceCollection collection)
        {
            collection.AddScoped<IUserService, UserService>();
        }
    }
}
