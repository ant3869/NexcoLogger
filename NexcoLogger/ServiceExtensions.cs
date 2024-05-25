using Microsoft.Extensions.DependencyInjection;
using NexcoLogger.Services;
using NexcoLogger.ViewModels;

namespace NexcoLogger
{
    public static class ServiceExtensions
    {
        public static void AddNexLog(this IServiceCollection services)
        {
            services.AddSingleton<LogsViewModel>();
            services.AddSingleton<ILoggingService, LoggingService>();
        }
    }
}
