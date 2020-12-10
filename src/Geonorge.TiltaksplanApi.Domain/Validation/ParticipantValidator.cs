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
                .When(participant => participant.OrganizationId == 0)
                .WithMessage(activity => localizer["Participant"]);

            RuleFor(participant => participant.OrganizationId)
                .Must(organizationId => organizationId > 0)
                .When(participant => string.IsNullOrEmpty(participant.Name))
                .WithMessage(activity => localizer["Participant"]);
        }
    }
}
