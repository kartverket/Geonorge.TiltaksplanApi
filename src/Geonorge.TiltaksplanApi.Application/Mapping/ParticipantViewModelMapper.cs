using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ParticipantViewModelMapper : IViewModelMapper<Participant, ParticipantViewModel>
    {
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;

        public ParticipantViewModelMapper(
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper)
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
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }
    }
}
