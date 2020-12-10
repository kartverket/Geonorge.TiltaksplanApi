using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class OrganizationConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder
                .ToTable("Organizations")
                .HasKey(organization => organization.Id);

            builder
                .Property(organization => organization.Id)
                .ValueGeneratedOnAdd();

            builder
                .Ignore(participant => participant.ValidationResult);
        }
    }
}
