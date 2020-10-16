using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActionPlanViewModelMapper : IViewModelMapper<ActionPlan, ActionPlanViewModel>
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
                Name = viewModel.Name,
                Progress = viewModel.Progress,
                Volume = viewModel.Volume,
                Status = viewModel.Status,
                TrafficLight = viewModel.TrafficLight,
                Results = viewModel.Results,
                Comment = viewModel.Comment,
                Activities = viewModel.Activities?
                    .ConvertAll(activity => _activityViewModelMapper.MapToDomainModel(activity))
            };
        }

        public ActionPlanViewModel MapToViewModel(ActionPlan domainModel)
        {
            if (domainModel == null)
                return null;

            return new ActionPlanViewModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Progress = domainModel.Progress,
                Volume = domainModel.Volume,
                Status = domainModel.Status,
                TrafficLight = domainModel.TrafficLight,
                Results = domainModel.Results,
                Comment = domainModel.Comment,
                Activities = domainModel.Activities?
                    .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity))
            };
        }
    }
}
