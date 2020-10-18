using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActionPlanViewModelMapper : IActionPlanViewModelMapper
    {
        private readonly IViewModelMapper<Activity, ActivityViewModel> _activityViewModelMapper;

        public ActionPlanViewModelMapper(
            IViewModelMapper<Activity, ActivityViewModel> activityViewModelMapper)
        {
            _activityViewModelMapper = activityViewModelMapper;
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
                .SingleOrDefault(translation => translation.Language.Culture == culture);

            if (translation == null)
                return null;

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
                Culture = translation.LanguageCulture,
                Activities = domainModel.Activities?
                    .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity))
            };
        }
    }
}
