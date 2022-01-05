using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Localization;

namespace Geonorge.TiltaksplanApi.Application.Validation
{
    public class MeasureValidator : AbstractValidator<Measure>
    {
        public MeasureValidator(
            IStringLocalizer<ValidationResource> localizer,
            IMeasureQuery measureQuery)
        {
            RuleFor(measure => measure.No)
                .NotEmpty()
                .WithMessage(measure => localizer["Number"]);

            RuleFor(measure => measure.No)
                .MustAsync(async (measure, no, cancellation) => await measureQuery.IsNumberAvailable(measure.Id, measure.No))
                .WithMessage(measure => localizer["NumberUniqueMeasure", measure.No]);

            RuleFor(measure => measure.OwnerId)
                .Must(ownerId => ownerId > 0)
                .WithMessage(measure => localizer["Owner"]);

            RuleFor(measure => measure.Volume)
                .InclusiveBetween(0, 5)
                .When(measure => measure.Volume.HasValue)
                .WithMessage(measure => localizer["Volume"]);

            RuleFor(measure => (int) measure.Status)
                .InclusiveBetween(1, 7)
                .When(measure => measure.Status.HasValue)
                .WithMessage(measure => localizer["StatusMeasure"]);

            RuleFor(measure => (int) measure.TrafficLight)
                .InclusiveBetween(1, 3)
                .When(measure => measure.TrafficLight.HasValue)
                .WithMessage(measure => localizer["TrafficLight"]);

            RuleFor(measure => measure.Results)
                .InclusiveBetween(1, 5)
                .When(measure => measure.Results.HasValue)
                .WithMessage(measure => localizer["Results"]);

            RuleForEach(measure => measure.Translations)
                .SetValidator(new MeasureTranslationValidator(localizer))
                .When(measure => measure.Translations != null);
        }
    }
}
