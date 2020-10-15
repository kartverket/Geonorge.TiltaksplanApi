using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityFrameworkConfiguration
{
    internal static class ActionPlanConfiguration
    {
        internal static void Configure(EntityTypeBuilder<ActionPlan> builder)
        {
            builder
                .ToTable("ActionPlans")
                .HasKey(actionPlan => actionPlan.Id);

            builder
                .HasMany(actionPlan => actionPlan.Activities)
                .WithOne()
                .HasForeignKey(activity => activity.ActionPlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(actionPlan => actionPlan.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
