using Geonorge.TiltaksplanApi.Domain.Attributes;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public enum TrafficLight
    {
        [LocalizedDescription("Red")]
        Red = 1,
        [LocalizedDescription("Yellow")]
        Yellow = 2,
        [LocalizedDescription("Green")]
        Green = 3
    }

    public enum PlanStatus
    {
        [LocalizedDescription("StartUp")]
        StartUp = 1,
        [LocalizedDescription("Investigation")]
        Investigation = 2,
        [LocalizedDescription("WorkingOut")]
        WorkingOut = 3,
        [LocalizedDescription("Concluding")]
        Concluding = 4,
        [LocalizedDescription("Done")]
        Done = 5,
        [LocalizedDescription("Expires")]
        Expires = 6,
        [LocalizedDescription("IncludedOther")]
        IncludedOther = 7
    }
}
