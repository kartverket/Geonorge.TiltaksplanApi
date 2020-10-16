using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
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
            return _context.ActionPlans.AsQueryable();
        }

        public async Task<ActionPlan> GetByIdAsync(int id)
        {
            return await GetAll()
                .SingleOrDefaultAsync(actionPlan => actionPlan.Id == id);
        }

        public ActionPlan Create(ActionPlan actionPlan)
        {
            _context.ActionPlans.Add(actionPlan);

            return actionPlan;
        }

        public void Delete(ActionPlan actionPlan)
        {
            if (actionPlan == null)
                return;

            _context.ActionPlans.Remove(actionPlan);
        }
    }
}
