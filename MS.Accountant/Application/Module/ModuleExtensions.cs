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
            services.AddSingleton<ITaxService, TaxService>();
            services.AddScoped<ITaxPayerService, TaxPayerService>();
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
