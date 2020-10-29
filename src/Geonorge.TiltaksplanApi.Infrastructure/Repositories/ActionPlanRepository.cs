using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.Repositories
{
    public class ActionPlanRepository : IMeasureRepository
    {
        private readonly MeasurePlanContext _context;

        public ActionPlanRepository(
            MeasurePlanContext context)
        {
            _context = context;
        }

        public IQueryable<Measure> GetAll()
        {
            return _context.Measures
                .AsQueryable();
        }

        public async Task<Measure> GetByIdAsync(int id)
        {
            return await GetAll()
                .Include(actionPlan => actionPlan.Translations)
                    .ThenInclude(translation => translation.Language)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .SingleOrDefaultAsync(actionPlan => actionPlan.Id == id);
        }

        public Measure Create(Measure actionPlan)
        {
            _context.Measures.Add(actionPlan);

            return actionPlan;
        }

        public void Delete(Measure actionPlan)
        {
            if (actionPlan == null)
                return;

            _context.Measures.Remove(actionPlan);
        }
    }
}
