using FluentEmail.Core;

namespace SimpleAccountSystem.Mvc.Services.FluentEmail
{
    public class FluentEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        public FluentEmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail= fluentEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await _fluentEmail.To(to)
                .Subject(subject)
                .Body(body)
                .SendAsync();
        }
    }
}
