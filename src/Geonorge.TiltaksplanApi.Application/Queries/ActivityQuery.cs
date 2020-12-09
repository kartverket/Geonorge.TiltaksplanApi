using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _defaultCulture;

        public ActivityQuery(
            MeasurePlanContext context,
            IActivityViewModelMapper activityViewModelMapper,
            IMeasureQuery measureQuery,
            IConfiguration configuration)
        {
            _context = context;
            _activityViewModelMapper = activityViewModelMapper;
            _measureQuery = measureQuery;
            _defaultCulture = configuration.GetValue<string>("DefaultCulture");
        }

        public async Task<IList<ActivityViewModel>> GetAllAsync(string culture)
        {
            var cult = GetCulture(culture);

            var activities = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.Translations
                    .Any(translation => translation.LanguageCulture == cult))
                .OrderBy(activity => activity.No)
                .ToListAsync();

            var viewModels = activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, cult));

            foreach (var viewModel in viewModels)
                await CompleteDataForViewModel(viewModel);

            return viewModels;
        }

        public async Task<ActivityViewModel> GetByIdAsync(int id, string culture)
        {
            var cult = GetCulture(culture);

            var activity = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .SingleOrDefaultAsync(activity => activity.Id == id && activity.Translations
                    .Any(translation => translation.LanguageCulture == cult));

            var viewModel = _activityViewModelMapper.MapToViewModel(activity, cult);

            await CompleteDataForViewModel(viewModel);

            return viewModel;
        }

        public async Task<List<ActivityViewModel>> GetByMeasureIdAsync(int measureId, string culture)
        {
            var cult = GetCulture(culture);

            var activities = await _context.Activities
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.MeasureId == measureId && activity.Translations
                    .Any(translation => translation.LanguageCulture == cult))
                .OrderBy(activity => activity.No)
                .ToListAsync();

            var viewModels = activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, cult));

            foreach (var viewModel in viewModels)
                await CompleteDataForViewModel(viewModel);

            return viewModels;
        }

        private async Task CompleteDataForViewModel(ActivityViewModel viewModel)
        {
            if (viewModel.MeasureId == 0)
                return;

            var measure = await _measureQuery.GetByIdAsync(viewModel.MeasureId, null);
            viewModel.ResponsibleAgency = measure?.Owner;
        }

        private string GetCulture(string culture) => !string.IsNullOrEmpty(culture) ? culture : _defaultCulture;
    }
}
