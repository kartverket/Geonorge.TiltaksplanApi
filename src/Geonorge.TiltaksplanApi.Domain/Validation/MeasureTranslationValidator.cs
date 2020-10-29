using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class MeasureTranslationValidator : AbstractValidator<MeasureTranslation>
    {
        public MeasureTranslationValidator()
        {
            RuleFor(measureTranslation => measureTranslation.Name)
                .NotEmpty();
            RuleFor(measureTranslation => measureTranslation.Progress)
                .NotEmpty();
            RuleFor(measureTranslation => measureTranslation.LanguageCulture)
                .NotEmpty();
        }
    }
}
