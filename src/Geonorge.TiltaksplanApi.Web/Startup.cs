using Geonorge.TiltaksplanApi.Application;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using Geonorge.TiltaksplanApi.Infrastructure.Repositories;
using Geonorge.TiltaksplanApi.Web;
using Geonorge.TiltaksplanApi.Web.Configuration;
using Geonorge.TiltaksplanApi.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Geonorge.TiltaksplanApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            SerilogConfiguration.ConfigureSerilog(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddEntityFrameworkForActionPlan(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlProvider, ActionPlanUrlProvider>();

            // Application services
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddTransient<IActionPlanService, ActionPlanService>();
            services.AddTransient<IActivityService, ActivityService>();

            // Queries
            services.AddTransient<IActionPlanQuery, ActionPlanQuery>();

            // Repositories
            services.AddScoped<IActionPlanRepository, ActionPlanRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Mappers
            services.AddTransient<IViewModelMapper<Activity, ActivityViewModel>, ActivityViewModelMapper>();
            services.AddTransient<IActionPlanViewModelMapper, ActionPlanViewModelMapper>();
            services.AddTransient<IViewModelMapper<Participant, ParticipantViewModel>, ParticipantViewModelMapper>();
            services.AddTransient<IViewModelMapper<Language, LanguageViewModel>, LanguageViewModelMapper>();
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

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);

            hostApplicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ActionPlanContext>();
            
            context.Database.Migrate();

            DataSeeder.SeedLanguages(context);
        }
    }
}
