using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ActivityTranslationValidator : AbstractValidator<ActivityTranslation>
    {
        public ActivityTranslationValidator()
        {
            RuleFor(activityTranslation => activityTranslation.Name)
                .NotEmpty();
            RuleFor(activityTranslation => activityTranslation.Title)
                .NotEmpty();
            RuleFor(activityTranslation => activityTranslation.LanguageCulture)
                .NotEmpty();
        }
    }
}
