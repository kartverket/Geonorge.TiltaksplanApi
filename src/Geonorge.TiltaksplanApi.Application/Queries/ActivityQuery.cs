using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class ActivityQuery : IActivityQuery
    {
        private readonly ActionPlanContext _context;
        private readonly IActivityViewModelMapper _activityViewModelMapper;

        public ActivityQuery(
            ActionPlanContext context,
            IActivityViewModelMapper activityViewModelMapper)
        {
            _context = context;
            _activityViewModelMapper = activityViewModelMapper;
        }

        public async Task<IList<ActivityViewModel>> GetAllAsync(string culture)
        {
            var actionPlans = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                .AsNoTracking()
                .Where(activity => activity.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            return actionPlans
                .ConvertAll(actionPlan => _activityViewModelMapper.MapToViewModel(actionPlan, culture));
        }

        public async Task<ActivityViewModel> GetByIdAsync(int id, string culture)
        {
            var actionPlan = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(activity => activity.Id == id && activity.Translations
                    .Any(translation => translation.LanguageCulture == culture));

            return _activityViewModelMapper.MapToViewModel(actionPlan, culture);
        }
    }
}
