using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public abstract class ViewModelWithValidation
    {
        protected ViewModelWithValidation()
        {
            ValidationErrors = new List<ValidationErrorViewModel>();
        }

        public List<ValidationErrorViewModel> ValidationErrors { get; set; }
        public bool HasErrors => HasOwnErrors() || HasNestedErrors();

        private bool HasOwnErrors()
        {
            return ValidationErrors?.Any() ?? false;
        }

        public List<ValidationErrorViewModel> AllValidationErrors()
        {
            var errors = new List<ValidationErrorViewModel>();

            errors.AddRange(ValidationErrors);

            var nestedViewModels = GetNestedViewModelsWithValidation();

            foreach (var viewModel in nestedViewModels)
            {
                if (viewModel == null)
                    continue;

                errors.AddRange(viewModel.AllValidationErrors());
            }

            return errors;
        }

        public string AllErrorCodes()
        {
            return string.Join(", ", AllValidationErrors()
                .Select(validationError => validationError.ErrorCode));
        }

        private bool HasNestedErrors()
        {
            var viewModels = GetNestedViewModelsWithValidation()
                .Where(vm => vm != null);
            
            return viewModels
                .Any(viewModel => viewModel.ValidationErrors.Any() || viewModel.HasNestedErrors());
        }

        private IEnumerable<ViewModelWithValidation> GetNestedViewModelsWithValidation()
        {
            var viewModels = new List<ViewModelWithValidation>();

            var propertyEntities = GetType().GetProperties()
                .Where(x => x.PropertyType.IsSubclassOf(typeof(ViewModelWithValidation)))
                .Select(property => property.GetValue(this) as ViewModelWithValidation);

            viewModels.AddRange(propertyEntities);

            var listEntities = GetType().GetProperties()
                .Where(p =>
                    p.PropertyType.IsGenericType &&
                    (p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                     p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) ||
                     p.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) &&
                    p.PropertyType.GetGenericArguments().Any(arg => arg.IsSubclassOf(typeof(ViewModelWithValidation))))
                .Select(property => property.GetValue(this))
                .OfType<IEnumerable<ViewModelWithValidation>>()
                .SelectMany(property => property);

            viewModels.AddRange(listEntities);

            return viewModels;
        }
    }
}
