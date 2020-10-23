using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public abstract class ValidatableEntity : EntityBase
    {
        protected ValidatableEntity()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public List<ValidationError> ValidationErrors { get; }
        public bool HasErrors => ValidationErrors.Any() || HasNestedErrors();

        public void AddValidationError(string property, string errorCode)
        {
            var error = new ValidationError(property, errorCode);

            if (!ValidationErrors.Contains(error))
                ValidationErrors.Add(error);
        }

        public List<ValidationError> AllValidationErrors()
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidationErrors);

            var nestedEntites = GetNestedEntities();

            foreach (var entity in nestedEntites)
            {
                if (entity == null)
                    continue;

                errors.AddRange(entity.AllValidationErrors());
            }

            return errors;
        }

        private IEnumerable<ValidatableEntity> GetNestedEntities()
        {
            var entities = new List<ValidatableEntity>();

            var propertyEntities = GetType().GetProperties()
                .Where(x => x.PropertyType.IsSubclassOf(typeof(ValidatableEntity)))
                .Select(property => property.GetValue(this) as ValidatableEntity);

            entities.AddRange(propertyEntities);

            var listEntities = GetType().GetProperties()
                .Where(p =>
                    p.PropertyType.IsGenericType &&
                    (p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                     p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) ||
                     p.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) &&
                    p.PropertyType.GetGenericArguments().Any(arg => arg.IsSubclassOf(typeof(ValidatableEntity))))
                .Select(property => property.GetValue(this))
                .OfType<IEnumerable<ValidatableEntity>>()
                .SelectMany(property => property);

            entities.AddRange(listEntities);

            return entities;
        }

        private bool HasNestedErrors()
        {
            var entities = GetNestedEntities().Where(entity => entity != null);
            return entities.Any(entity => entity.ValidationErrors.Any() || entity.HasNestedErrors());
        }
    }
}
