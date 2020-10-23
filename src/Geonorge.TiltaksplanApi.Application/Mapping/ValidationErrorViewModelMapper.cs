using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ValidationErrorViewModelMapper : IViewModelMapper<ValidationError, ValidationErrorViewModel>
    {
        public ValidationError MapToDomainModel(ValidationErrorViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public ValidationErrorViewModel MapToViewModel(ValidationError domainModel)
        {
            if (domainModel == null)
                return null;

            return new ValidationErrorViewModel
            {
                Property = domainModel.Property,
                ErrorCode = domainModel.ErrorCode
            };
        }
    }
}