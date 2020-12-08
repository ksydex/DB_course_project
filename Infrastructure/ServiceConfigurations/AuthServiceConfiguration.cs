using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractAndProjectManager.Infrastructure.ServiceConfigurations
{
    public static class AuthServiceConfiguration
    {
        public static IServiceCollection ConfigureAuthService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = new PathString("/");
                    opts.AccessDeniedPath = new PathString("/");
                });
            return services;
        }
    }
}