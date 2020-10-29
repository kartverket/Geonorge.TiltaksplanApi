using FluentValidation;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Domain.Validation
{
    public class MeasureValidator : AbstractValidator<Measure>
    {
        public MeasureValidator()
        {
            RuleFor(measure => measure.Volume)
                .InclusiveBetween(0, 5);
            RuleFor(measure => measure.Status)
                .InclusiveBetween(1, 5);
            RuleFor(measure => (int) measure.TrafficLight)
                .InclusiveBetween(1, 3);
            RuleFor(measure => measure.Translations)
                .NotNull()
                .Must(translation => translation.Count == 1);
            RuleForEach(measure => measure.Translations)
                .NotNull()
                .SetValidator(new MeasureTranslationValidator());
        }
    }
}
