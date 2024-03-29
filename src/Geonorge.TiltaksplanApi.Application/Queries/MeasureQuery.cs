﻿using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID;
using Geonorge.TiltaksplanApi.Domain.Models;
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

        public async Task<IList<MeasureViewModel>> GetAllAsync(string culture, string organization)
        {
            var cult = GetCulture(culture);

            var measures = await _context.Measures
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .Where(measure => measure.Translations
                    .Any(translation => translation.LanguageCulture == GetCulture(cult)))
                .OrderBy(measure => measure.No)
                .ToListAsync();

            if (!string.IsNullOrEmpty(organization))
                measures = measures.Where(o => o.Owner.Name.ToLower() == organization.ToLower()).ToList();

            var viewModels = measures
                .ConvertAll(measure => _measureViewModelMapper.MapToViewModel(measure, cult));

            viewModels.ForEach(CompleteDataForViewModel);

            return viewModels;
        }

        public async Task<MeasureViewModel> GetByIdAsync(int id, string culture)
        {
            var cult = GetCulture(culture);

            var measure = await _context.Measures
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(measure => measure.Id == id && 
                    measure.Translations.Any(translation => translation.LanguageCulture == cult));

            var viewModel = _measureViewModelMapper.MapToViewModel(measure, cult);

            CompleteDataForViewModel(viewModel);

            return viewModel;
        }

        public async Task<MeasureViewModel> GetByNumberAsync(int number, string culture)
        {
            var cult = GetCulture(culture);

            var measure = await _context.Measures
                .Include(measure => measure.Owner)
                .Include(measure => measure.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Translations)
                .Include(measure => measure.Activities)
                    .ThenInclude(activity => activity.Participants)
                .AsNoTracking()
                .SingleOrDefaultAsync(measure => measure.No == number &&
                    measure.Translations.Any(translation => translation.LanguageCulture == cult));

            var viewModel = _measureViewModelMapper.MapToViewModel(measure, cult);

            CompleteDataForViewModel(viewModel);

            return viewModel;
        }

        public async Task<bool> HasOwnership(int id, long orgNumber)
        {
            var measure = await _context.Measures
                .Include(measure => measure.Owner)
                .AsNoTracking()
                .SingleOrDefaultAsync(measure => measure.Id == id);

            if (measure == null)
                return false;

            return measure.Owner.OrgNumber == orgNumber;
        }

        public async Task<bool> IsNumberAvailable(int id, int number)
        {
            return await _context.Measures
                .AllAsync(measure => measure.No != number || measure.Id == id);
        }

        private void CompleteDataForViewModel(MeasureViewModel viewModel)
        {
            viewModel.Activities
                .ForEach(activity => activity.ResponsibleAgency = viewModel.Owner);
        }

        private string GetCulture(string culture) => !string.IsNullOrEmpty(culture) ? culture : _defaultCulture;
    }
}
