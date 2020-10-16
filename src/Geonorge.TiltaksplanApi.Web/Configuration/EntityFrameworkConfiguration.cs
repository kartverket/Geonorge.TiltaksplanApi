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
        public static IServiceCollection AddEntityFrameworkForActionPlan(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<ActionPlanContext>(options =>
                options.UseSqlServer(configuration["EntityFramework:ActionPlanContext:ConnectionString"], builder =>
                    builder.MigrationsAssembly("Geonorge.TiltaksplanApi.Infrastructure")));

            return services;
        }
    }

    internal class ActionPlanContextFactory : IDesignTimeDbContextFactory<ActionPlanContext>
    {
        public ActionPlanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ActionPlanContext>()
                .UseSqlServer(configuration["EntityFramework:ActionPlanContext:ConnectionString"], builder =>
                    builder.MigrationsAssembly("Geonorge.TiltaksplanApi.Infrastructure"));

            return new ActionPlanContext(optionsBuilder.Options);
        }
    }
}
