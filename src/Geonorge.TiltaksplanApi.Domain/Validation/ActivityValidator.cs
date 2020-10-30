using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;
using System;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator(IStringLocalizer<ValidationResource> localizer)
        {
            RuleFor(activity => activity.ImplementationStart)
                .Must(BeValidDate)
                .WithMessage(activity => localizer["ImplementationStart"]);
           
            RuleFor(activity => activity.ImplementationEnd)
                .Must(BeValidDate)
                .WithMessage(activity => localizer["ImplementationEnd"]);

            RuleFor(activity => activity.ImplementationEnd)
                .GreaterThanOrEqualTo(activity => activity.ImplementationStart)
                .WithMessage(activity => localizer["ImplementationEndBeforeImplementationStart"]);

            RuleFor(activity => activity.Status)
                .NotEmpty()
                .WithMessage(activity => localizer["StatusActivity"]);

            RuleFor(activity => activity.Translations)
                .Must(activityTranslations => activityTranslations != null && activityTranslations.Count > 0);
            
            RuleForEach(activity => activity.Translations)
                .SetValidator(new ActivityTranslationValidator(localizer));

            RuleFor(activity => activity.Participants)
              .Must(participants => participants != null && participants.Count > 0)
              .WithMessage(activity => localizer["Participants"]);

            RuleForEach(activity => activity.Participants)
                .SetValidator(new ParticipantValidator(localizer));
        }

        private bool BeValidDate(DateTime date) => !date.Equals(default);
    }
}
