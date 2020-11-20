using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Extensions;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ParticipantViewModelMapper : IViewModelMapper<Participant, ParticipantViewModel>
    {
        private readonly IViewModelMapper<Organization, OrganizationViewModel> _organizationViewModelMapper;
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;

        public ParticipantViewModelMapper(
            IViewModelMapper<Organization, OrganizationViewModel> organizationViewModelMapper,
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper)
        {
            _organizationViewModelMapper = organizationViewModelMapper;
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
                Name = viewModel.Name.ToModel(),
                OrganizationId = viewModel.OrganizationId,
            };
        }

        public ParticipantViewModel MapToViewModel(Participant domainModel)
        {
            if (domainModel == null)
                return null;

            var organization = _organizationViewModelMapper.MapToViewModel(domainModel.Organization);

            return new ParticipantViewModel
            {
                Id = domainModel.Id,
                ActivityId = domainModel.ActivityId,
                OrganizationId = domainModel.OrganizationId,
                Name = organization?.Name ?? domainModel.Name,
                OrgNumber = organization?.OrgNumber,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }
    }
}
