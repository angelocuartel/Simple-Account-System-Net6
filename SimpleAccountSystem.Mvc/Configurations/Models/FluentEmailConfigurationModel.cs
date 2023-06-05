namespace SimpleAccountSystem.Mvc.Configurations.Models
{
    public class FluentEmailConfigurationModel
    {
        public bool SmtpUseDefaultCredentials { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string? SmtpHost { get; set; }
        public string? EmailUserName { get; set; }
        public string? EmailPassword { get; set; }
    }
}
