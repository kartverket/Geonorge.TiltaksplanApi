using System;
using System.IO;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.TiltaksplanApi.Web.Configuration
{
    public static class EntityFrameworkConfiguration
    {
        public static IServiceCollection AddEntityFrameworkForMeasurePlan(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<MeasurePlanContext>(options =>
                options.UseSqlServer(configuration["EntityFramework:MeasurePlanContext:ConnectionString"], builder =>
                    builder.MigrationsAssembly("Geonorge.TiltaksplanApi.Infrastructure")));

            return services;
        }
    }

    internal class MeasureContextFactory : IDesignTimeDbContextFactory<MeasurePlanContext>
    {
        public MeasurePlanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MeasurePlanContext>()
                .UseSqlServer(configuration["EntityFramework:MeasurePlanContext:ConnectionString"], builder =>
                    builder.MigrationsAssembly("Geonorge.TiltaksplanApi.Infrastructure"));

            return new MeasurePlanContext(optionsBuilder.Options);
        }
    }
}
