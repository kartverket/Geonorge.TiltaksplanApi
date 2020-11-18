using System;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class Participant : ValidatableEntity
    {
        public int ActivityId { get; set; }
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
