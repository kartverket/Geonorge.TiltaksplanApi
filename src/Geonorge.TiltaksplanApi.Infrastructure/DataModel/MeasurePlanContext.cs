using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel
{
    public class MeasurePlanContext : DbContext
    {
        public MeasurePlanContext()
        {
        }

        public MeasurePlanContext(
            DbContextOptions<MeasurePlanContext> options) : base(options)
        {
        }

        public DbSet<Measure> Measures { get; set; }
        public DbSet<MeasureTranslation> MeasureTranslations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityTranslation> ActivityTranslations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Language> Languages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Measure>(MeasureConfiguration.Configure);
            builder.Entity<MeasureTranslation>(MeasureTranslationConfiguration.Configure);
            builder.Entity<Activity>(ActivityConfiguration.Configure);
            builder.Entity<ActivityTranslation>(ActivityTranslationConfiguration.Configure);
            builder.Entity<Participant>(ParticipantConfiguration.Configure);
            builder.Entity<Language>(LanguageConfiguration.Configure);

            base.OnModelCreating(builder);
        }
    }
}
