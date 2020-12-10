using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public abstract void Update(EntityBase updatedEntity);

        protected void UpdateList<T>(List<T> existingEntities, IReadOnlyCollection<T> updatedEntities) where T : EntityBase
        {
            RemoveDeleted(existingEntities, updatedEntities);
            UpdateRest(existingEntities, updatedEntities);
            AddCreated(existingEntities, updatedEntities);
        }

        protected void RemoveDeleted<T>(List<T> existingEntities, IReadOnlyCollection<T> updatedEntities) where T : EntityBase
        {
            if (updatedEntities == null)
                return;

            var deletedEntities = existingEntities
                .Where(original => updatedEntities.All(updated => updated.Id != original.Id))
                .ToList();

            foreach (var deleted in deletedEntities)
                existingEntities.Remove(deleted);
        }

        protected void AddCreated<T>(List<T> existingEntities, IReadOnlyCollection<T> updatedEntities) where T : EntityBase
        {
            if (updatedEntities == null)
                return;

            var createdEntities = updatedEntities
                .Where(oppdatert => existingEntities.All(originalt => originalt.Id != oppdatert.Id))
                .ToList();

            existingEntities.AddRange(createdEntities);
        }

        protected void UpdateRest<T>(List<T> existingEntities, IReadOnlyCollection<T> updatedEntities) where T : EntityBase
        {
            if (updatedEntities == null)
                return;

            existingEntities
                .ForEach(existing =>
                {
                    var updated = updatedEntities.SingleOrDefault(updated => updated.Id == existing.Id);

                    if (updated != null)
                        existing.Update(updated);
                });
        }
    }
}
