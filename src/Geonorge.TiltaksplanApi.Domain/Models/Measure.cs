using System.Collections.Generic;

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

            AddCreated(Translations, updated.Translations);
            UpdateRest(Translations, updated.Translations);
        }
    }
}
