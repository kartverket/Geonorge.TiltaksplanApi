using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly MeasurePlanContext _context;

        public ActivityRepository(
            MeasurePlanContext context)
        {
            _context = context;
        }

        public IQueryable<Activity> GetAll()
        {
            return _context.Activities
                .AsQueryable();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await GetAll()
                .Include(activity => activity.Translations)
                    .ThenInclude(translation => translation.Language)
                .Include(activity => activity.Participants)
                .SingleOrDefaultAsync(activity => activity.Id == id);
        }

        public Activity Create(Activity activity)
        {
            _context.Activities.Add(activity);

            return activity;
        }

        public void Delete(Activity activity)
        {
            if (activity == null)
                return;

            _context.Activities.Remove(activity);
        }
    }
}
