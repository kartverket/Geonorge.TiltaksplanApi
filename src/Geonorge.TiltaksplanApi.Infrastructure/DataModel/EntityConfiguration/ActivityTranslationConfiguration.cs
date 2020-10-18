using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class ActivityTranslationConfiguration
    {
        internal static void Configure(EntityTypeBuilder<ActivityTranslation> builder)
        {
            builder
                .ToTable("ActivityTranslations")
                .HasKey(activityTranslation => activityTranslation.Id);

            builder
                .Property(activityTranslation => activityTranslation.Name)
                .IsRequired();

            builder
                .Property(activityTranslation => activityTranslation.Title)
                .IsRequired();

            builder
                .Property(activityTranslation => activityTranslation.LanguageCulture)
                .IsRequired();

            builder
                .HasOne(activityTranslation => activityTranslation.Language)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(activityTranslation => activityTranslation.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
