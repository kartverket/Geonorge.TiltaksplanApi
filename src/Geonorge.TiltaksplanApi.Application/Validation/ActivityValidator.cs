using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;
using System;

namespace Geonorge.TiltaksplanApi.Application.Validation
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator(
            IStringLocalizer<ValidationResource> localizer,
            IActivityQuery activityQuery)
        {
            RuleFor(activity => activity.No)
                .NotEmpty()
                .WithMessage(activity => localizer["Number"]);

            RuleFor(activity => activity.No)
                .MustAsync(async (activity, number, cancellation) => await activityQuery.IsNumberAvailable(activity.MeasureId, activity.Id, activity.No))
                .WithMessage(activity => localizer["NumberUniqueActivity", activity.No]);

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
