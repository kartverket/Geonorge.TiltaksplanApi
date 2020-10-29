using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using System;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity.ImplementationStart)
                .Must(BeValidDate);
            RuleFor(activity => activity.ImplementationEnd)
                .Must(BeValidDate)
                .GreaterThanOrEqualTo(activity => activity.ImplementationStart);
            RuleFor(activity => activity.Status)
                .NotEmpty();
            RuleFor(activity => activity.Translations)
                .NotNull()
                .Must(activity => activity.Count == 1);
            RuleForEach(activity => activity.Translations)
                .NotNull()
                .SetValidator(new ActivityTranslationValidator());
            RuleForEach(activity => activity.Participants)
                .SetValidator(new ParticipantValidator());
        }

        private bool BeValidDate(DateTime date) => !date.Equals(default);
    }
}
