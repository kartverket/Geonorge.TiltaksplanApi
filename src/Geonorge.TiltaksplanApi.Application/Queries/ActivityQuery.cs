﻿using Geonorge.TiltaksplanApi.Application.Mapping;
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

        public ActivityQuery(
            MeasurePlanContext context,
            IActivityViewModelMapper activityViewModelMapper)
        {
            _context = context;
            _activityViewModelMapper = activityViewModelMapper;
        }

        public async Task<IList<ActivityViewModel>> GetAllAsync(string culture)
        {
            var activities = await _context.Activities
                .Include(activity => activity.ResponsibleAgency)
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            return activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, culture));
        }

        public async Task<ActivityViewModel> GetByIdAsync(int id, string culture)
        {
            var activity = await _context.Activities
                .Include(activity => activity.ResponsibleAgency)
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .SingleOrDefaultAsync(activity => activity.Id == id && activity.Translations
                    .Any(translation => translation.LanguageCulture == culture));

            return _activityViewModelMapper.MapToViewModel(activity, culture);
        }

        public async Task<List<ActivityViewModel>> GetByMeasureIdAsync(int measureId, string culture)
        {
            var activities = await _context.Activities
                .Include(activity => activity.ResponsibleAgency)
                .Include(activity => activity.Translations)
                .Include(activity => activity.Participants)
                    .ThenInclude(participant => participant.Organization)
                .AsNoTracking()
                .Where(activity => activity.MeasureId == measureId && activity.Translations
                    .Any(translation => translation.LanguageCulture == culture))
                .ToListAsync();

            return activities
                .ConvertAll(activity => _activityViewModelMapper.MapToViewModel(activity, culture));
        }
    }
}
