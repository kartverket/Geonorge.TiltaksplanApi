using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityFrameworkConfiguration
{
    internal static class ActivityConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder
                .ToTable("Activities")
                .HasKey(activity => activity.Id);

            builder
                .Property(activity => activity.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
