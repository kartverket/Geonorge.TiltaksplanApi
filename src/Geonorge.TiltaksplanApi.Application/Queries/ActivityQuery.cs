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
        private readonly MeasurePlanContext _context;
        private readonly IActivityViewModelMapper _activityViewModelMapper;
        private readonly IMeasureQuery _measureQuery;

        public ActivityQuery(
            MeasurePlanContext context,
            IActivityViewModelMapper activityViewModelMapper,
            IMeasureQuery measureQuery)
        {
            _context = context;
            _activityViewModelMapper = activityViewModelMapper;
            _measureQuery = measureQuery;
        }

        public async Task<IList<ActivityViewModel>> GetAllAsync(string culture)
        {
            var activities = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            var activityViewModels = activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, culture));

            foreach (var viewModel in activityViewModels)
                await CompleteDataForViewModel(viewModel);

            return activityViewModels;
        }

        public async Task<ActivityViewModel> GetByIdAsync(int id, string culture)
        {
            var activity = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .SingleOrDefaultAsync(activity => activity.Id == id && activity.Translations
                    .Any(translation => translation.LanguageCulture == culture));

            var activityViewModel = _activityViewModelMapper.MapToViewModel(activity, culture);

            await CompleteDataForViewModel(activityViewModel);

            return activityViewModel;
        }

        public async Task<List<ActivityViewModel>> GetByMeasureIdAsync(int measureId, string culture)
        {
            var activities = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.MeasureId == measureId && activity.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            var activityViewModels = activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, culture));

            foreach (var viewModel in activityViewModels)
                await CompleteDataForViewModel(viewModel);

            return activityViewModels;
        }

        private async Task CompleteDataForViewModel(ActivityViewModel viewModel)
        {
            if (viewModel.MeasureId == 0)
                return;

            var measure = await _measureQuery.GetByIdAsync(viewModel.MeasureId, null);
            viewModel.ResponsibleAgency = measure?.Owner;
        }
    }
}
