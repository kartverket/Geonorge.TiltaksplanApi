using System;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Activity : ValidatableEntity
    {
        public int MeasureId { get; set; }
        public int No { get; set; }
        public DateTime ImplementationStart { get; set; }
        public DateTime ImplementationEnd { get; set; }
        public List<Participant> Participants { get; set; }
        public PlanStatus Status { get; set; }
        public List<ActivityTranslation> Translations { get; set; }
        public DateTime LastUpdated { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Activity) updatedEntity;

            if (No != updated.No)
                No = updated.No;

            if (ImplementationStart != updated.ImplementationStart)
                ImplementationStart = updated.ImplementationStart;

            if (ImplementationEnd != updated.ImplementationEnd)
                ImplementationEnd = updated.ImplementationEnd;

            if (Status != updated.Status)
                Status = updated.Status;

            RemoveDeleted(Participants, updated.Participants);
            AddCreated(Participants, updated.Participants);

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
