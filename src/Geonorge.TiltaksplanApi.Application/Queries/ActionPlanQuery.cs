using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class ActionPlanQuery : IActionPlanQuery
    {
        private readonly ActionPlanContext _context;
        private readonly IViewModelMapper<ActionPlan, ActionPlanViewModel> _actionPlanViewModelMapper;

        public ActionPlanQuery(
            ActionPlanContext context,
            IViewModelMapper<ActionPlan, ActionPlanViewModel> actionPlanViewModelMapper)
        {
            _context = context;
            _actionPlanViewModelMapper = actionPlanViewModelMapper;
        }

        public async Task<IList<ActionPlanViewModel>> GetAllAsync()
        {
            var actionPlans = await _context.ActionPlans
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .ToListAsync();

            return actionPlans.ConvertAll(actionPlan => _actionPlanViewModelMapper.MapToViewModel(actionPlan));
        }

        public async Task<ActionPlanViewModel> GetByIdAsync(int id)
        {
            var actionPlan = await _context.ActionPlans
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(actionPlan => actionPlan.Id == id);

            return _actionPlanViewModelMapper.MapToViewModel(actionPlan);
        }
    }
}
