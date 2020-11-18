using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class MeasureConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Measure> builder)
        {
            builder
                .ToTable("Measures")
                .HasKey(measure => measure.Id);

            builder
                .HasOne(measure => measure.Owner)
                .WithOne()
                .HasForeignKey<Measure>(measure => measure.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(measure => measure.Translations)
                .WithOne()
                .HasForeignKey(measureTranslation => measureTranslation.MeasureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(measure => measure.Activities)
                .WithOne()
                .HasForeignKey(activity => activity.MeasureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(measure => measure.Id)
                .ValueGeneratedOnAdd();

            builder
                .Ignore(measure => measure.ValidationResult);
        }
    }
}
