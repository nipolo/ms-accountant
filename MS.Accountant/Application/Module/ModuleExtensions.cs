using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MS.Accountant.Application.Services;
using MS.Accountant.Application.Services.Abstractions;
using MS.Accountant.Domain.Entities;
using MS.Accountant.Domain.Module;

namespace MS.Accountant.Application.Module
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainModule(configuration);

            services.AddScoped<ITaxPayerService, TaxPayerService>();
            services.AddSingleton<ICacheService<TaxPayer>, CacheService<TaxPayer>>();

            return services;
        }
    }
}
