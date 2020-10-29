using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class MeasureTranslationConfiguration
    {
        internal static void Configure(EntityTypeBuilder<MeasureTranslation> builder)
        {
            builder
                .ToTable("MeasureTranslations")
                .HasKey(measureTranslation => new { measureTranslation.MeasureId, measureTranslation.LanguageCulture });

            builder
                .Property(measureTranslation => measureTranslation.Name)
                .IsRequired();

            builder
                .Property(measureTranslation => measureTranslation.Progress)
                .IsRequired();

            builder
                .Property(measureTranslation => measureTranslation.LanguageCulture)
                .IsRequired();

            builder
                .HasOne(measureTranslation => measureTranslation.Language)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(measureTranslation => measureTranslation.Id)
                .ValueGeneratedOnAdd();

            builder
                .Ignore(measureTranslation => measureTranslation.ValidationResult);
        }
    }
}
