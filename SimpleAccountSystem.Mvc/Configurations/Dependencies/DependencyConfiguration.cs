using SimpleAccountSystem.Domain.Service;
using SimpleAccountSystem.Mvc.Attributes;

namespace SimpleAccountSystem.Mvc.Configurations.Dependencies
{
    public static class DependencyConfiguration
    {
        public static void InjectDependencies(this IServiceCollection collection)
        {
            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<GlobalExceptionHandlerMiddleWare>();
        }
    }
}
