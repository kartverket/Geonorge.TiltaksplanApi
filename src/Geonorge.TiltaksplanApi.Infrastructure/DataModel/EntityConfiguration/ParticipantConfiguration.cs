using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.EntityConfiguration
{
    internal static class ParticipantConfiguration
    {
        internal static void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder
                .ToTable("Participants")
                .HasKey(participant => participant.Id);

            builder
                .Property(participant => participant.Id)
                .ValueGeneratedOnAdd();

            builder
                .Ignore(participant => participant.ValidationResult);
        }
    }
}
