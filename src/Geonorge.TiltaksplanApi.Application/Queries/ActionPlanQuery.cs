using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class ActionPlanQuery : IActionPlanQuery
    {
        private readonly ActionPlanContext _context;
        private readonly IActionPlanViewModelMapper _actionPlanViewModelMapper;

        public ActionPlanQuery(
            ActionPlanContext context,
            IActionPlanViewModelMapper actionPlanViewModelMapper)
        {
            _context = context;
            _actionPlanViewModelMapper = actionPlanViewModelMapper;
        }

        public async Task<IList<ActionPlanViewModel>> GetAllAsync(string culture)
        {
            var actionPlans = await _context.ActionPlans
                .Include(actionPlan => actionPlan.Translations)
                    .ThenInclude(actionPlan => actionPlan.Language)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .ToListAsync();

            return actionPlans.ConvertAll(actionPlan => _actionPlanViewModelMapper.MapToViewModel(actionPlan, culture));
        }

        public async Task<ActionPlanViewModel> GetByIdAsync(int id, string culture)
        {
            var actionPlan = await _context.ActionPlans
                .Include(actionPlan => actionPlan.Translations)
                    .ThenInclude(actionPlan => actionPlan.Language)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(actionPlan => actionPlan.Id == id);

            return _actionPlanViewModelMapper.MapToViewModel(actionPlan, culture);
        }
    }
}
