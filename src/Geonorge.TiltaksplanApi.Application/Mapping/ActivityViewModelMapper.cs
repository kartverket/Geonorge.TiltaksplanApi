using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActivityViewModelMapper : IViewModelMapper<Activity, ActivityViewModel>
    {
        public Activity MapToDomainModel(ActivityViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public ActivityViewModel MapToViewModel(Activity domainModel)
        {
            if (domainModel == null)
                return null;

            return new ActivityViewModel
            {
                Id = domainModel.Id,
                ActionPlanId = domainModel.ActionPlanId,
                Name = domainModel.Name
            };
        }
    }
}
