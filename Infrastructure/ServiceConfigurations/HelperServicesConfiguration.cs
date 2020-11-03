using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContractAndProjectManager.Infrastructure.ServiceConfigurations
{
    public static class HelperServicesConfiguration
    {
        public static IServiceCollection ConfigureHelperServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<PasswordService>();
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            return services;
        }
    }
}