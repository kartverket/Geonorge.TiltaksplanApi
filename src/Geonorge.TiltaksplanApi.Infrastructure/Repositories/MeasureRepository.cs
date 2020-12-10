using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.Repositories
{
    public class MeasureRepository : IMeasureRepository
    {
        private readonly MeasurePlanContext _context;

        public MeasureRepository(
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
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                    .ThenInclude(translation => translation.Language)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .SingleOrDefaultAsync(measure => measure.Id == id);
        }

        public Measure Create(Measure measure)
        {
            _context.Measures.Add(measure);

            return measure;
        }

        public void Delete(Measure measure)
        {
            if (measure == null)
                return;

            _context.Measures.Remove(measure);
        }
    }
}
