namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Organization : ValidatableEntity
    {
        public string Name { get; set; }
        public long? OrgNumber { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Organization) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (OrgNumber != updated.OrgNumber)
                OrgNumber = updated.OrgNumber;
        }
    }
}
