using Geonorge.TiltaksplanApi.Application.Configuration;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class MeasureViewModel : ViewModelWithValidation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Progress { get; set; }
        public int Volume { get; set; }
        public int Status { get; set; }
        public TrafficLight TrafficLight { get; set; }
        public string Results { get; set; }
        public string Comment { get; set; }
        [SwaggerIgnore]
        public List<ActivityViewModel> Activities { get; set; }
        public string Culture { get; set; }
        [JsonIgnore]
        public int MeasureTranslationId { get; set; }
    }
}
