using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class MeasureViewModelMapper : IMeasureViewModelMapper
    {
        private readonly IActivityViewModelMapper _activityViewModelMapper;
        private readonly IViewModelMapper<Organization, OrganizationViewModel> _organizationViewModelMapper;
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;
        private readonly string _defaultCulture;

        public MeasureViewModelMapper(
            IActivityViewModelMapper activityViewModelMapper,
            IViewModelMapper<Organization, OrganizationViewModel> organizationViewModelMapper,
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper,
            IConfiguration configuration)
        {
            _activityViewModelMapper = activityViewModelMapper;
            _organizationViewModelMapper = organizationViewModelMapper;
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
            _defaultCulture = configuration.GetValue<string>("DefaultCulture");
        }

        public Measure MapToDomainModel(MeasureViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Measure
            {
                Id = viewModel.Id,
                OwnerId = viewModel.Owner?.Id ?? 0,
                No = viewModel.No,
                Volume = viewModel.Volume,
                Status = viewModel.Status,
                TrafficLight = viewModel.TrafficLight,
                Results = viewModel.Results,
                Translations = new List<MeasureTranslation>
                {
                    new MeasureTranslation
                    {
                        Id = viewModel.MeasureTranslationId,
                        MeasureId = viewModel.Id,
                        Name = viewModel.Name,
                        Progress = viewModel.Progress,
                        Comment = viewModel.Comment,
                        LanguageCulture = GetCulture(viewModel.Culture)
                    }
                }
            };
        }

        public MeasureViewModel MapToViewModel(Measure domainModel, string culture)
        {
            if (domainModel == null)
                return null;

            var translation = domainModel.Translations
                .SingleOrDefault(translation => translation.LanguageCulture == GetCulture(culture));

            var activities = domainModel.Activities?
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, GetCulture(culture)));

            activities?.RemoveAll(activity => activity == null);

            return new MeasureViewModel
            {
                Id = domainModel.Id,
                No = domainModel.No,
                Name = translation.Name,
                Owner = _organizationViewModelMapper.MapToViewModel(domainModel.Owner),
                Progress = translation?.Progress,
                Volume = domainModel.Volume ?? 0,
                Status = domainModel.Status ?? PlanStatus.StartUp,
                TrafficLight = domainModel.TrafficLight ?? TrafficLight.Red,
                Results = domainModel.Results ?? 1,
                Comment = translation?.Comment,
                MeasureTranslationId = translation?.Id ?? 0,
                Culture = translation?.LanguageCulture,
                Activities = activities,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }

        private string GetCulture(string culture) => !string.IsNullOrEmpty(culture) ? culture : _defaultCulture;
    }
}
