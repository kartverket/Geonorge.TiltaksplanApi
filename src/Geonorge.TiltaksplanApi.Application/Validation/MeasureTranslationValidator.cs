using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Application.Validation
{
    public class MeasureTranslationValidator : AbstractValidator<MeasureTranslation>
    {
        public MeasureTranslationValidator(IStringLocalizer<ValidationResource> localizer)
        {
            RuleFor(measureTranslation => measureTranslation.Name)
                .NotEmpty()
                .WithMessage(measure => localizer["Name"]);

            RuleFor(measureTranslation => measureTranslation.LanguageCulture)
                .NotEmpty()
                .WithMessage(measure => localizer["LanguageCulture"]);
        }
    }
}
