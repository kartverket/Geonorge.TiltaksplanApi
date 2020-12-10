using FluentValidation.Results;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class OrganizationViewModelMapper : IViewModelMapper<Organization, OrganizationViewModel>
    {
        private readonly IViewModelMapper<ValidationResult, List<string>> _validationErrorViewModelMapper;

        public OrganizationViewModelMapper(
            IViewModelMapper<ValidationResult, List<string>> validationErrorViewModelMapper)
        {
            _validationErrorViewModelMapper = validationErrorViewModelMapper;
        }

        public Organization MapToDomainModel(OrganizationViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Organization
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                OrgNumber = viewModel.OrgNumber
            };
        }

        public OrganizationViewModel MapToViewModel(Organization domainModel)
        {
            if (domainModel == null)
                return null;

            return new OrganizationViewModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                OrgNumber = domainModel.OrgNumber,
                ValidationErrors = _validationErrorViewModelMapper
                    .MapToViewModel(domainModel.ValidationResult)
            };
        }
    }
}
