using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class ValidationErrorViewModelMapper : IViewModelMapper<ValidationResult, List<string>>
    {
        public ValidationResult MapToDomainModel(List<string> errorMessages)
        {
            throw new System.NotImplementedException();
        }

        public List<string> MapToViewModel(ValidationResult domainModel)
        {
            if (domainModel == null)
                return null;

            return domainModel.Errors?
                .Select(validationFailure => validationFailure.ErrorMessage)
                .ToList();
        }
    }
}