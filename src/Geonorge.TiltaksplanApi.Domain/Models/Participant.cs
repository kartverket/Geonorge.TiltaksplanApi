namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Participant : ValidatableEntity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (Participant) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;
        }
    }
}
