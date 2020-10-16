using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ParticipantViewModelMapper : IViewModelMapper<Participant, ParticipantViewModel>
    {
        public Participant MapToDomainModel(ParticipantViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public ParticipantViewModel MapToViewModel(Participant domainModel)
        {
            if (domainModel == null)
                return null;

            return new ParticipantViewModel
            {
                Id = domainModel.Id,
                ActivityId = domainModel.ActivityId,
                Name = domainModel.Name
            };
        }
    }
}
