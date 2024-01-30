namespace SimpleAccountSystem.Mvc.Configurations.Models
{
    public class SentryGlobalConfigurationModel
    {
        public string? Dsn { get; set; }
        public bool Debug { get; set; }
        public bool AutoSessionTracking { get; set; }
        public bool IsGlobalModeEnabled { get; set; }
        public bool EnableTracing { get; set; }
        public byte TracesSampleRate { get; set; }
    }
}
