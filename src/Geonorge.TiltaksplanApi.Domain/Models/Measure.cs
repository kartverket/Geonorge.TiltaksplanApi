using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Measure : ValidatableEntity
    {
        public int Volume { get; set; }
        public int Status { get; set; }
        public TrafficLight TrafficLight { get; set; }
        public List<Activity> Activities { get; set; }
        public List<MeasureTranslation> Translations { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Measure) updatedEntity;

            if (Volume != updated.Volume)
                Volume = updated.Volume;

            if (Status != updated.Status)
                Status = updated.Status;

            if (TrafficLight != updated.TrafficLight)
                TrafficLight = updated.TrafficLight;

            UpdateTranslations(updated.Translations);
        }

        private void UpdateTranslations(List<MeasureTranslation> updatedTranslations)
        {
            var updatedTranslation = updatedTranslations.SingleOrDefault();

            if (updatedTranslation == null)
                return;

            var existingTranslation = Translations
                .SingleOrDefault(translation => translation.LanguageCulture == updatedTranslation.LanguageCulture);

            if (existingTranslation == null)
                Translations.Add(updatedTranslation);
            else
                existingTranslation.Update(updatedTranslation);
        }
    }
}
