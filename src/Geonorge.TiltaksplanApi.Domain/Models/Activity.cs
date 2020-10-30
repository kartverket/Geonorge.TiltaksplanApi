using System;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Activity : ValidatableEntity
    {
        public int MeasureId { get; set; }
        public DateTime ImplementationStart { get; set; }
        public DateTime ImplementationEnd { get; set; }
        public List<Participant> Participants { get; set; }
        public ActivityStatus Status { get; set; }
        public List<ActivityTranslation> Translations { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Activity) updatedEntity;

            if (ImplementationStart != updated.ImplementationStart)
                ImplementationStart = updated.ImplementationStart;

            if (ImplementationEnd != updated.ImplementationEnd)
                ImplementationEnd = updated.ImplementationEnd;

            if (Status != updated.Status)
                Status = updated.Status;

            UpdateList(Participants, updated.Participants);

            UpdateTranslations(updated.Translations);
        }

        private void UpdateTranslations(List<ActivityTranslation> updatedTranslations)
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
