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
    public class MeasureQuery : IMeasureQuery
    {
        private readonly MeasurePlanContext _context;
        private readonly IMeasureViewModelMapper _measureViewModelMapper;
        private readonly string _defaultCulture;

        public MeasureQuery(
            MeasurePlanContext context,
            IMeasureViewModelMapper measureViewModelMapper,
            IConfiguration configuration)
        {
            _context = context;
            _measureViewModelMapper = measureViewModelMapper;
            _defaultCulture = configuration.GetValue<string>("DefaultCulture");
        }

        public async Task<IList<MeasureViewModel>> GetAllAsync(string culture)
        {
            var measures = await _context.Measures
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .Where(measure => measure.Translations
                    .Any(translation => translation.LanguageCulture == GetCulture(culture)))
                .ToListAsync();

            return measures
                .ConvertAll(measure => _measureViewModelMapper.MapToViewModel(measure, culture));
        }

        public async Task<MeasureViewModel> GetByIdAsync(int id, string culture)
        {
            var measure = await _context.Measures
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(measure => measure.Id == id && 
                    measure.Translations.Any(translation => translation.LanguageCulture == GetCulture(culture)));

            return _measureViewModelMapper.MapToViewModel(measure, culture);
        }

        private string GetCulture(string culture) => !string.IsNullOrEmpty(culture) ? culture : _defaultCulture;
    }
}
