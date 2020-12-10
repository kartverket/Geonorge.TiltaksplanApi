namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class OrganizationViewModel : ViewModelWithValidation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? OrgNumber { get; set; }
    }
}
