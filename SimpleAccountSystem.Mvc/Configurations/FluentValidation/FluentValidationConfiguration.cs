using FluentValidation;
using FluentValidation.AspNetCore;

namespace SimpleAccountSystem.Mvc.Configurations.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection service)
        {
            service.AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters()
                   .AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
