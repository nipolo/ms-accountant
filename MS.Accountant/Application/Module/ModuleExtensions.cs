using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MS.Accountant.Application.Services;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Module
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<ITaxPayerService, TaxPayerService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ITaxSettingsService, TaxSettingsService>();

            return services;
        }
    }
}
