namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ParticipantViewModel : ViewModelWithValidation
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Name { get; set; }
    }
}
