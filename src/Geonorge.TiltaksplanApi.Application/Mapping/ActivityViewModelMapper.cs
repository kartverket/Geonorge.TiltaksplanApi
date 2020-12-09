using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActivityViewModelMapper : IActivityViewModelMapper
    {
        private readonly IViewModelMapper<Organization, OrganizationViewModel> _organizationViewModelMapper;
        private readonly IViewModelMapper<Participant, ParticipantViewModel> _participantViewModelMapper;
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;
        private readonly string _defaultCulture;

        public ActivityViewModelMapper(
            IViewModelMapper<Organization, OrganizationViewModel> organizationViewModelMapper,
            IViewModelMapper<Participant, ParticipantViewModel> participantViewModelMapper,
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper,
            IConfiguration configuration)
        {
            _organizationViewModelMapper = organizationViewModelMapper;
            _participantViewModelMapper = participantViewModelMapper;
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
            _defaultCulture = configuration.GetValue<string>("DefaultCulture");
        }

        public Activity MapToDomainModel(ActivityViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Activity
            {
                Id = viewModel.Id,
                MeasureId = viewModel.MeasureId,
                No = viewModel.No,
                ImplementationStart = viewModel.ImplementationStart,
                ImplementationEnd = viewModel.ImplementationEnd,
                Status = viewModel.Status,
                Participants = viewModel.Participants?
                    .ConvertAll(participant => _participantViewModelMapper.MapToDomainModel(participant)),
                Translations = new List<ActivityTranslation>
                {
                    new ActivityTranslation
                    {
                        Id = viewModel.ActivityTranslationId,
                        ActivityId = viewModel.Id,
                        LanguageCulture = GetCulture(viewModel.Culture),
                        Name = viewModel.Name,
                        Description = viewModel.Description
                    }
                }
            };
        }

        public ActivityViewModel MapToViewModel(Activity domainModel, string culture)
        {
            if (domainModel == null)
                return null;

            var translation = domainModel.Translations
                .SingleOrDefault(translation => translation.LanguageCulture == GetCulture(culture));

            return new ActivityViewModel
            {
                Id = domainModel.Id,
                MeasureId = domainModel.MeasureId,
                No = domainModel.No,
                Name = translation?.Name,
                Description = translation?.Description,
                ImplementationStart = domainModel.ImplementationStart,
                ImplementationEnd = domainModel.ImplementationEnd,
                Participants = domainModel.Participants?
                    .ConvertAll(participant => _participantViewModelMapper.MapToViewModel(participant)),
                Status = domainModel.Status,
                Culture = translation?.LanguageCulture,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }

        private string GetCulture(string culture) => !string.IsNullOrEmpty(culture) ? culture : _defaultCulture;
    }
}
