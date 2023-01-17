using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MS.Accountant.Domain.Services;
using MS.Accountant.Domain.Services.Abstractions;

namespace MS.Accountant.Domain.Module
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaxService, TaxService>();
            services.AddSingleton<ITaxSettingsService, TaxSettingsService>();

            return services;
        }
    }
}
