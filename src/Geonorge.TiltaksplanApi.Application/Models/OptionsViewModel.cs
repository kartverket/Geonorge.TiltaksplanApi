using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class OptionsViewModel
    {
        public IEnumerable<SelectOption> TrafficLights { get; set; }
        public IEnumerable<SelectOption> PlanStatuses { get; set; }
        public IEnumerable<SelectOption> MeasureVolume { get; set; }
        public IEnumerable<SelectOption> MeasureResults { get; set; }
    }
}
