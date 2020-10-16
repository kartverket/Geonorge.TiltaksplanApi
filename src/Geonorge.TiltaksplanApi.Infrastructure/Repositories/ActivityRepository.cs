using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using System;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ActionPlanContext _context;

        public ActivityRepository(ActionPlanContext context)
        {
            _context = context;
        }

        public Task CreateAsync(Activity domainObject)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Activity domainObject)
        {
            throw new NotImplementedException();
        }
    }
}
