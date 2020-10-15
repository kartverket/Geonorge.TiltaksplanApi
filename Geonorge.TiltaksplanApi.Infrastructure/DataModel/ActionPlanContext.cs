using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityFrameworkConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel
{
    public class ActionPlanContext : DbContext
    {
        public ActionPlanContext()
        {
        }

        public ActionPlanContext(
            DbContextOptions<ActionPlanContext> options) : base(options)
        {
        }

        public DbSet<ActionPlan> ActionPlans { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<ActionPlan>(ActionPlanConfiguration.Configure);
            builder.Entity<Activity>(ActivityConfiguration.Configure);

            base.OnModelCreating(builder);
        }
    }
}
