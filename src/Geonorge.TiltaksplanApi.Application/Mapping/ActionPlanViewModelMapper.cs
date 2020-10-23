using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActionPlanViewModelMapper : IActionPlanViewModelMapper
    {
        private readonly IActivityViewModelMapper _activityViewModelMapper;
        private readonly IViewModelMapper<ValidationError, ValidationErrorViewModel> _validationErrorViewModelMapper;

        public ActionPlanViewModelMapper(
            IActivityViewModelMapper activityViewModelMapper,
            IViewModelMapper<ValidationError, ValidationErrorViewModel> validationErrorViewModelMapper)
        {
            _activityViewModelMapper = activityViewModelMapper;
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
        }

        public ActionPlan MapToDomainModel(ActionPlanViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new ActionPlan
            {
                Id = viewModel.Id,
                Volume = viewModel.Volume,
                Status = viewModel.Status,
                TrafficLight = viewModel.TrafficLight,
                Translations = new List<ActionPlanTranslation>
                {
                    new ActionPlanTranslation
                    {
                        Id = viewModel.ActionPlanTranslationId,
                        ActionPlanId = viewModel.Id,
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

        public ActionPlanViewModel MapToViewModel(ActionPlan domainModel, string culture)
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

            return new ActionPlanViewModel
            {
                Id = domainModel.Id,
                Name = translation.Name,
                Progress = translation.Progress,
                Volume = domainModel.Volume,
                Status = domainModel.Status,
                TrafficLight = domainModel.TrafficLight,
                Results = translation.Results,
                Comment = translation.Comment,
                ActionPlanTranslationId = translation.Id,
                Culture = translation.LanguageCulture,
                Activities = activities,
                ValidationErrors = domainModel.ValidationErrors?
                    .ConvertAll(validationError => _validationErrorViewModelMapper.MapToViewModel(validationError))
            };
        }
    }
}
