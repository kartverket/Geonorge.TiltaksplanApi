using Geonorge.TiltaksplanApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        public int ActionPlanId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ImplementationStart { get; set; }
        public DateTime ImplementationEnd { get; set; }
        public List<ParticipantViewModel> Participants { get; set; }
        public ActivityStatus Status { get; set; }
        public string Culture { get; set; }
    }
}
