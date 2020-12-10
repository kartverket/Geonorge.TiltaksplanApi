using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class ActivityTranslationValidator : AbstractValidator<ActivityTranslation>
    {
        public ActivityTranslationValidator(IStringLocalizer<ValidationResource> localizer)
        {
            RuleFor(activityTranslation => activityTranslation.Name)
                .NotEmpty()
                .WithMessage(activity => localizer["Name"]);

            RuleFor(activityTranslation => activityTranslation.LanguageCulture)
                .NotEmpty()
                .WithMessage(activity => localizer["LanguageCulture"]);
        }
    }
}
