using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class ActionPlan : EntityBase
    {
        public string Name { get; set; }
        public string Progress { get; set; }
        public int Volume { get; set; }
        public int Status { get; set; }
        public TrafficLight TrafficLight { get; set; }
        public string Results { get; set; }
        public string Comment { get; set; }
        public List<Activity> Activities { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (ActionPlan) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (Progress != updated.Progress)
                Progress = updated.Progress;

            if (Volume != updated.Volume)
                Volume = updated.Volume;

            if (Status != updated.Status)
                Status = updated.Status;

            if (TrafficLight != updated.TrafficLight)
                TrafficLight = updated.TrafficLight;

            if (Results != updated.Results)
                Results = updated.Results;

            if (Comment != updated.Comment)
                Comment = updated.Comment;

            UpdateList(Activities, updated.Activities);
        }
    }
}
