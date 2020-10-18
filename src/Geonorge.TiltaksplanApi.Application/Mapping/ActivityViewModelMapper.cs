using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ActivityViewModelMapper : IViewModelMapper<Activity, ActivityViewModel>
    {
        private readonly IViewModelMapper<Participant, ParticipantViewModel> _participantViewModelMapper;

        public ActivityViewModelMapper(
            IViewModelMapper<Participant, ParticipantViewModel> participantViewModelMapper)
        {
            _participantViewModelMapper = participantViewModelMapper;
        }

        public Activity MapToDomainModel(ActivityViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            /*return new Activity
            {
                Id = viewModel.Id,
                ActionPlanId = viewModel.ActionPlanId,
                Name = viewModel.Name,
                Title = viewModel.Title,
                Description = viewModel.Description,
                ImplementationStart = viewModel.ImplementationStart,
                ImplementationEnd = viewModel.ImplementationEnd,
                Participants = viewModel.Participants?
                    .ConvertAll(participant => _participantViewModelMapper.MapToDomainModel(participant)),
                Status = viewModel.Status
            };*/

            return null;
        }

        public ActivityViewModel MapToViewModel(Activity domainModel)
        {
            if (domainModel == null)
                return null;

            /*return new ActivityViewModel
            {
                Id = domainModel.Id,
                ActionPlanId = domainModel.ActionPlanId,
                Name = domainModel.Name,
                Title = domainModel.Title,
                Description = domainModel.Description,
                ImplementationStart = domainModel.ImplementationStart,
                ImplementationEnd = domainModel.ImplementationEnd,
                Participants = domainModel.Participants?
                    .ConvertAll(participant => _participantViewModelMapper.MapToViewModel(participant)),
                Status = domainModel.Status
            };*/

            return null;
        }
    }
}
