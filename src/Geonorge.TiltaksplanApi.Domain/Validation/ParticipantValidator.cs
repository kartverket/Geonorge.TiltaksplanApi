using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ParticipantValidator : AbstractValidator<Participant>
    {
        public ParticipantValidator(IStringLocalizer<ValidationResource> localizer)
        {
            RuleFor(participant => participant.Name)
                .NotEmpty()
                .WithMessage(activity => localizer["Name"]);
        }
    }
}
