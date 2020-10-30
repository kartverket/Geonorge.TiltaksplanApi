using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class MeasureViewModelMapper : IMeasureViewModelMapper
    {
        private readonly IActivityViewModelMapper _activityViewModelMapper;
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;

        public MeasureViewModelMapper(
            IActivityViewModelMapper activityViewModelMapper,
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper)
        {
            _activityViewModelMapper = activityViewModelMapper;
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
        }

        public Measure MapToDomainModel(MeasureViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Measure
            {
                Id = viewModel.Id,
                Volume = viewModel.Volume,
                Status = viewModel.Status,
                TrafficLight = viewModel.TrafficLight,
                Translations = new List<MeasureTranslation>
                {
                    new MeasureTranslation
                    {
                        Id = viewModel.MeasureTranslationId,
                        MeasureId = viewModel.Id,
                        LanguageCulture = viewModel.Culture,
                        Name = viewModel.Name,
                        Progress = viewModel.Progress,
                        Results = viewModel.Results,
                        Comment = viewModel.Comment
                    }
                },
                Activities = viewModel.Activities?
                    .ConvertAll(activity => _activityViewModelMapper.MapToDomainModel(activity))
            };
        }

        public MeasureViewModel MapToViewModel(Measure domainModel, string culture)
        {
            if (domainModel == null)
                return null;

            var translation = domainModel.Translations
                .SingleOrDefault(translation => translation.LanguageCulture == culture);

            if (translation == null)
                return null;

            var activities = domainModel.Activities?
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, culture));

            activities?.RemoveAll(activity => activity == null);

            return new MeasureViewModel
            {
                Id = domainModel.Id,
                Name = translation.Name,
                Progress = translation.Progress,
                Volume = domainModel.Volume,
                Status = domainModel.Status,
                TrafficLight = domainModel.TrafficLight,
                Results = translation.Results,
                Comment = translation.Comment,
                MeasureTranslationId = translation.Id,
                Culture = translation.LanguageCulture,
                Activities = activities,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }
    }
}
