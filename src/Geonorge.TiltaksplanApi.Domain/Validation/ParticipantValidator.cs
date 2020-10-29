using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ParticipantValidator : AbstractValidator<Participant>
    {
        public ParticipantValidator()
        {
            RuleFor(participant => participant.Name).NotEmpty();
        }
    }
}
