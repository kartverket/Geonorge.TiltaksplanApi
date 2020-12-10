using Geonorge.TiltaksplanApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ActivityViewModel : ViewModelWithValidation
    {
        public int Id { get; set; }
        public int MeasureId { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public OrganizationViewModel ResponsibleAgency { get; set; }
        public string Description { get; set; }
        public DateTime ImplementationStart { get; set; }
        public DateTime ImplementationEnd { get; set; }
        public List<ParticipantViewModel> Participants { get; set; }
        public PlanStatus Status { get; set; }
        public string Culture { get; set; }
        [JsonIgnore]
        public int ActivityTranslationId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
