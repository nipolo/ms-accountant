using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using MS.Accountant.Api.Exceptions;
using MS.Accountant.Application.Module;

namespace MS.Accountant.Api
{
    public class Program
    {
        private const string AllowAllPolicyName = "AllowAllPolicy";

        public static void Main(string[] args)
        {
            var app = SetupApplication(args);

            app.Run();
        }

        private static WebApplication SetupApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = BuildConfiguration();

            builder.Services.AddApplicationModule(config);

            AddServices(builder.Services);

            AddSettings(builder.Services, config);

            builder.Services
                .AddControllers(options =>
                    options.Filters.Add(typeof(HttpResponseExceptionFilter)))
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            if (builder.Environment.EnvironmentName == Module.Environments.Local
                || builder.Environment.EnvironmentName == Module.Environments.BETA)
            {
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();

            WebApplicationPostSetup(app);

            return app;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
        }

        private static void AddServices(IServiceCollection services)
        {
        }

        private static void AddSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TaxesSettings>(
                configuration.GetSection(TaxesSettings.Key));
        }

        private static void WebApplicationPostSetup(WebApplication app)
        {
            if (app.Environment.EnvironmentName == Module.Environments.Local
                || app.Environment.EnvironmentName == Module.Environments.BETA)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
        }
    }
}