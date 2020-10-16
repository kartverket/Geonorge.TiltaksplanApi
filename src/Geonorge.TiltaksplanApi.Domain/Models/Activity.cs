using System;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Activity : EntityBase
    {
        public int ActionPlanId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ImplementationStart { get; set; }
        public DateTime ImplementationEnd { get; set; }
        public List<Participant> Participants { get; set; }
        public ActivityStatus Status { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Activity) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (Title != updated.Title)
                Title = updated.Title;

            if (Description != updated.Description)
                Description = updated.Description;

            if (Status != updated.Status)
                Status = updated.Status;

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
