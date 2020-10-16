using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.Repositories
{
    public class ActionPlanRepository : IActionPlanRepository
    {
        private readonly ActionPlanContext _context;

        public ActionPlanRepository(ActionPlanContext context)
        {
            _context = context;
        }

        public IQueryable<ActionPlan> GetAll()
        {
            return _context.ActionPlans;
        }

        public Task<ActionPlan> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ActionPlan domainObject)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, ActionPlan domainObject)
        {
            throw new NotImplementedException();
        }
    }
}
