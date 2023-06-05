using Microsoft.Extensions.Configuration;
using SimpleAccountSystem.Mvc.Configurations.Constants;
using SimpleAccountSystem.Mvc.Configurations.Models;
using System.Net;

namespace SimpleAccountSystem.Mvc.Configurations.FluentEmail
{
    public static class FluentEmailConfiguration
    {
        public static void BuildFluentEmailDependencies(this IServiceCollection services,ConfigurationManager configManager)
        {
            FluentEmailConfigurationModel configModel = configManager.GetSection(ConfigurationConstant.FluentEmail).Get<FluentEmailConfigurationModel>();

            services.AddFluentEmail(configModel.EmailUserName).AddSmtpSender(new System.Net.Mail.SmtpClient(configModel.SmtpHost)
                                                                {
                                                                    UseDefaultCredentials = configModel.SmtpUseDefaultCredentials,
                                                                    Port = configModel.SmtpPort,
                                                                    EnableSsl = configModel.EnableSsl,
                                                                    Credentials = new NetworkCredential(configModel.EmailUserName,configModel.EmailPassword)
                                                                });
        }
    }
}
