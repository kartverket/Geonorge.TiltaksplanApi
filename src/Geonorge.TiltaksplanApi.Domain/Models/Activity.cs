using System;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Activity : ValidatableEntity
    {
        public int ActionPlanId { get; set; }
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
        }
    }
}
