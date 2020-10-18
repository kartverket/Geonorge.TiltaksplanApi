using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class ActionPlanTranslationConfiguration
    {
        internal static void Configure(EntityTypeBuilder<ActionPlanTranslation> builder)
        {
            builder
                .ToTable("ActionPlanTranslations")
                .HasKey(actionPlanTranslation => actionPlanTranslation.Id);

            builder
                .Property(actionPlanTranslation => actionPlanTranslation.Name)
                .IsRequired();

            builder
                .Property(actionPlanTranslation => actionPlanTranslation.Progress)
                .IsRequired();

            builder
                .Property(actionPlanTranslation => actionPlanTranslation.LanguageCulture)
                .IsRequired();

            builder
                .HasOne(actionPlanTranslation => actionPlanTranslation.Language)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(actionPlanTranslation => actionPlanTranslation.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
