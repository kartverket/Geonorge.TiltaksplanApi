using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ActionPlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ActivityViewModel> Activities { get; set; }
    }
}
