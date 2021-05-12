using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Exceptions;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services.Authorization;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IUnitOfWorkManager _uowManager;
        private readonly IMeasureRepository _measureRepository;
        private readonly IMeasureViewModelMapper _measureViewModelMapper;
        private readonly IValidator<Measure> _measureValidator;
        private readonly IOrganizationQuery _organizationQuery;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ExceptionResource> _localizer;

        public MeasureService(
            IUnitOfWorkManager uowManager,
            IMeasureRepository measureRepository,
            IMeasureViewModelMapper measureViewModelMapper,
            IValidator<Measure> measureValidator,
            IOrganizationQuery organizationQuery,
            IAuthorizationService authorizationService,
            IStringLocalizer<ExceptionResource> localizer)
        {
            _uowManager = uowManager;
            _measureRepository = measureRepository;
            _measureViewModelMapper = measureViewModelMapper;
            _measureValidator = measureValidator;
            _organizationQuery = organizationQuery;
            _authorizationService = authorizationService;
            _localizer = localizer;
        }

        public async Task<MeasureViewModel> CreateAsync(MeasureViewModel viewModel)
        {
            await _authorizationService.AuthorizeActivity(UserActivity.CreateMeasure);

            var measure = _measureViewModelMapper.MapToDomainModel(viewModel);

            if (await IsValid(measure))
            {
                using var uow = _uowManager.GetUnitOfWork();
                measure.LastUpdated = DateTime.Now;
                _measureRepository.Create(measure);
                await uow.SaveChangesAsync();
            }               

            var resultViewModel = _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel, measure);

            return resultViewModel;
        }

        public async Task<MeasureViewModel> UpdateAsync(int id, MeasureViewModel viewModel)
        {
            await _authorizationService.AuthorizeActivity(UserActivity.UpdateMeasure, id);

            var update = _measureViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            var measure = await _measureRepository
                .GetByIdAsync(id);            

            measure.Update(update);

            if (await IsValid(measure))
            {
                measure.LastUpdated = DateTime.Now;
                await uow.SaveChangesAsync();
            }

            var resultViewModel = _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel, measure);

            return resultViewModel;
        }

        public async Task DeleteAsync(int id)
        {
            await _authorizationService.AuthorizeActivity(UserActivity.DeleteMeasure);

            using var uow = _uowManager.GetUnitOfWork();
            var measure = await _measureRepository.GetByIdAsync(id);

            if (measure.Activities.Any())
                throw new WorkProcessException(_localizer["CannotDeleteMeasure", measure.Id]);

            _measureRepository.Delete(measure);
            await uow.SaveChangesAsync();
        }

        private async Task CompleteDataForViewModel(MeasureViewModel viewModel, Measure model)
        {
            viewModel.Owner = await _organizationQuery.GetByIdAsync(model.OwnerId);
        }

        private async Task<bool> IsValid(Measure measure)
        {
            measure.ValidationResult = await _measureValidator
                .ValidateAsync(measure);

            return measure.ValidationResult.IsValid;
        }
    }
}
