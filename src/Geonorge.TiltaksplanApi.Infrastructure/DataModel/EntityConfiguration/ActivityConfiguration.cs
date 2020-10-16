using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class ActivityConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder
                .ToTable("Activities")
                .HasKey(activity => activity.Id);

            builder
                .Property(activity => activity.Name)
                .IsRequired();

            builder
                .Property(activity => activity.Title)
                .IsRequired();

            builder
                .Property(activity => activity.ImplementationStart)
                .IsRequired();

            builder
                .Property(activity => activity.ImplementationEnd)
                .IsRequired();

            builder
                .Property(activity => activity.Status)
                .IsRequired();

            builder
                .HasMany(activity => activity.Participants)
                .WithOne()
                .HasForeignKey(participant => participant.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(activity => activity.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
