using FluentValidation;
using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application;
using Geonorge.TiltaksplanApi.Application.Configuration;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Domain.Validation;
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
using System.Collections.Generic;

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
            services.AddSwaggerGen(options => { options.SchemaFilter<SwaggerExcludePropertySchemaFilter>(); });
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddEntityFrameworkForMeasurePlan(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlProvider, MeasurePlanUrlProvider>();

            // Application services
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddTransient<IMeasureService, MeasureService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IParticipantService, ParticipantService>();

            // Validators
            services.AddTransient<IValidator<Measure>, MeasureValidator>();
            services.AddTransient<IValidator<Activity>, ActivityValidator>();
            services.AddTransient<IValidator<Participant>, ParticipantValidator>();

            // Queries
            services.AddTransient<IMeasureQuery, MeasureQuery>();
            services.AddTransient<IActivityQuery, ActivityQuery>();

            // Repositories
            services.AddScoped<IMeasureRepository, MeasureRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Mappers
            services.AddTransient<IActivityViewModelMapper, ActivityViewModelMapper>();
            services.AddTransient<IMeasureViewModelMapper, MeasureViewModelMapper>();
            services.AddTransient<IViewModelMapper<Participant, ParticipantViewModel>, ParticipantViewModelMapper>();
            services.AddTransient<IViewModelMapper<Language, LanguageViewModel>, LanguageViewModelMapper>();
            services.AddTransient<IViewModelMapper<ValidationResult, List<string>>, ValidationErrorViewModelMapper>();

            // Configuration
            services.Configure<ApiUrlsConfiguration>(Configuration.GetSection(ApiUrlsConfiguration.SectionName));
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

            app.UseSwagger();

            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Geonorge.Tiltaksplan API V1");
            });

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
            using var context = serviceScope.ServiceProvider.GetService<MeasurePlanContext>();
            
            context.Database.Migrate();

            DataSeeder.SeedLanguages(context);
        }
    }
}
