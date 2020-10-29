using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActivityViewModelMapper : IActivityViewModelMapper
    {
        private readonly IViewModelMapper<Participant, ParticipantViewModel> _participantViewModelMapper;
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;

        public ActivityViewModelMapper(
            IViewModelMapper<Participant, ParticipantViewModel> participantViewModelMapper,
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper)
        {
            _participantViewModelMapper = participantViewModelMapper;
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
        }

        public Activity MapToDomainModel(ActivityViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Activity
            {
                Id = viewModel.Id,
                ActionPlanId = viewModel.ActionPlanId,
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
                        LanguageCulture = viewModel.Culture,
                        Name = viewModel.Name,
                        Title = viewModel.Title,
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
                .SingleOrDefault(translation => translation.LanguageCulture == culture);

            if (translation == null)
                return null;

            return new ActivityViewModel
            {
                Id = domainModel.Id,
                ActionPlanId = domainModel.ActionPlanId,
                Name = translation.Name,
                Title = translation.Title,
                Description = translation.Description,
                ImplementationStart = domainModel.ImplementationStart,
                ImplementationEnd = domainModel.ImplementationEnd,
                Participants = domainModel.Participants?
                    .ConvertAll(participant => _participantViewModelMapper.MapToViewModel(participant)),
                Status = domainModel.Status,
                Culture = translation.LanguageCulture,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }
    }
}
