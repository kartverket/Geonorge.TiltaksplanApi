using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class MeasureValidator : AbstractValidator<Measure>
    {
        public MeasureValidator(IStringLocalizer<ValidationResource> localizer)
        {
            RuleFor(measure => measure.Volume)
                .InclusiveBetween(0, 5)
                .WithMessage(measure => localizer["Volume"]);

            RuleFor(measure => measure.Status)
                .InclusiveBetween(1, 5)
                .WithMessage(measure => localizer["StatusMeasure"]);

            RuleFor(measure => (int) measure.TrafficLight)
                .InclusiveBetween(1, 3)
                .WithMessage(measure => localizer["TrafficLight"]);

            RuleFor(measure => measure.Translations)
                .Must(measureTranslations => measureTranslations != null && measureTranslations.Count > 0);

            RuleForEach(measure => measure.Translations)
                .SetValidator(new MeasureTranslationValidator(localizer));
        }
    }
}
