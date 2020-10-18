using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class LanguageConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                .ToTable("Languages")
                .HasKey(language => language.Culture);

            builder
                .Property(language => language.Name)
                .IsRequired();
        }
    }
}
