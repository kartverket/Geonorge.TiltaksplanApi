using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .Where(actionPlan => actionPlan.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            return actionPlans
                .ConvertAll(actionPlan => _actionPlanViewModelMapper.MapToViewModel(actionPlan, culture));
        }

        public async Task<ActionPlanViewModel> GetByIdAsync(int id, string culture)
        {
            var actionPlan = await _context.ActionPlans
                .Include(actionPlan => actionPlan.Translations)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(actionPlan => actionPlan.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(actionPlan => actionPlan.Id == id && 
                    actionPlan.Translations.Any(translation => translation.LanguageCulture == culture));

            return _actionPlanViewModelMapper.MapToViewModel(actionPlan, culture);
        }
    }
}
