namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ParticipantViewModel : ViewModelWithValidation
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public int? OrganizationId { get; set; }
        public string Name { get; set; }
        public long? OrgNumber { get; set; }
    }
}
