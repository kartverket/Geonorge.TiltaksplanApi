using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ParticipantViewModelMapper : IViewModelMapper<Participant, ParticipantViewModel>
    {
        private readonly IViewModelMapper<ValidationError, ValidationErrorViewModel> _validationErrorViewModelMapper;
        
        public ParticipantViewModelMapper(
            IViewModelMapper<ValidationError, ValidationErrorViewModel> validationErrorViewModelMapper)
        {
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
        }

        public Participant MapToDomainModel(ParticipantViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Participant
            {
                Id = viewModel.Id,
                ActivityId = viewModel.ActivityId,
                Name = viewModel.Name
            };
        }

        public ParticipantViewModel MapToViewModel(Participant domainModel)
        {
            if (domainModel == null)
                return null;

            return new ParticipantViewModel
            {
                Id = domainModel.Id,
                ActivityId = domainModel.ActivityId,
                Name = domainModel.Name,
                ValidationErrors = domainModel.ValidationErrors?
                    .ConvertAll(validationError => _validationErrorViewModelMapper.MapToViewModel(validationError))
            };
        }
    }
}
