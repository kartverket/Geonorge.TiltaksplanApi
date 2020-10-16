using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using Geonorge.TiltaksplanApi.Infrastructure.Repositories;
using Geonorge.TiltaksplanApi.Web.Configuration;
using Geonorge.TiltaksplanApi.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddEntityFrameworkForActionPlan(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Application services
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddTransient<IActionPlanService, ActionPlanService>();
            services.AddTransient<IActivityService, ActivityService>();

            // Queries
            services.AddTransient<IAsyncQuery<IEnumerable<ActionPlanViewModel>>, ActionPlanQuery>();

            // Repositories
            services.AddScoped<IActionPlanRepository, ActionPlanRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Mappers
            services.AddTransient<IViewModelMapper<Activity, ActivityViewModel>, ActivityViewModelMapper>();
            services.AddTransient<IViewModelMapper<ActionPlan, ActionPlanViewModel>, ActionPlanViewModelMapper>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            ILoggerFactory loggerFactory,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            loggerFactory.AddSerilog(Log.Logger, true);

            app.UseMiddleware<RequestMetricsMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            hostApplicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
