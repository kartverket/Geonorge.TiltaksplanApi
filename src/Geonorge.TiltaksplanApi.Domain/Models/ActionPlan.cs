using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class ActionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
