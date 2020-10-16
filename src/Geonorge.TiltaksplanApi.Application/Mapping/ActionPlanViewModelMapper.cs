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
            throw new System.NotImplementedException();
        }

        public ActionPlanViewModel MapToViewModel(ActionPlan domainModel)
        {
            if (domainModel == null)
                return null;

            return new ActionPlanViewModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Activities = domainModel.Activities?
                    .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity))
            };
        }
    }
}
