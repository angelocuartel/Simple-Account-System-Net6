using Sentry;
using SimpleAccountSystem.Mvc.Configurations.Constants;
using SimpleAccountSystem.Mvc.Configurations.Models;

namespace SimpleAccountSystem.Mvc.Configurations.Sentry
{
    public static class SentryConfiguration
    {
        public static void Build(ConfigurationManager configurationManager)
        {
            var sentryConfig = configurationManager
                .GetSection(ConfigurationConstant.Sentry)
                .Get<SentryGlobalConfigurationModel>();

            SentrySdk.Init(opt =>
            {
                opt.Dsn = sentryConfig.Dsn;
                opt.AutoSessionTracking = sentryConfig.AutoSessionTracking;
                opt.IsGlobalModeEnabled = sentryConfig.IsGlobalModeEnabled;
                opt.EnableTracing = sentryConfig.EnableTracing;
                opt.TracesSampleRate = sentryConfig.TracesSampleRate;
                opt.Debug = sentryConfig.Debug;
            });
        }
    }
}
