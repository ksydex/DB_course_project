using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractAndProjectManager.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public static class AppSettingsExtensions
    {
        public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(config);
            return services;
        }
    }
}