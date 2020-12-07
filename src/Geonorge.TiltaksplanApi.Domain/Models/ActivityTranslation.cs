namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class ActivityTranslation : ValidatableEntity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LanguageCulture { get; set; }
        public Language Language { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (ActivityTranslation) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (Description != updated.Description)
                Description = updated.Description;
        }
    }
}
