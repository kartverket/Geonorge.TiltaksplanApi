using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public class MeasureQuery : IMeasureQuery
    {
        private readonly MeasurePlanContext _context;
        private readonly IMeasureViewModelMapper _measureViewModelMapper;

        public MeasureQuery(
            MeasurePlanContext context,
            IMeasureViewModelMapper measureViewModelMapper)
        {
            _context = context;
            _measureViewModelMapper = measureViewModelMapper;
        }

        public async Task<IList<MeasureViewModel>> GetAllAsync(string culture)
        {
            var measures = await _context.Measures
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .Where(measure => measure.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            return measures
                .ConvertAll(measure => _measureViewModelMapper.MapToViewModel(measure, culture));
        }

        public async Task<MeasureViewModel> GetByIdAsync(int id, string culture)
        {
            var measure = await _context.Measures
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(measure => measure.Id == id && 
                    measure.Translations.Any(translation => translation.LanguageCulture == culture));

            return _measureViewModelMapper.MapToViewModel(measure, culture);
        }
    }
}
