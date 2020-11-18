using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Exceptions;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<ExceptionResource> _localizer;

        public MeasureService(
            IUnitOfWorkManager uowManager,
            IMeasureRepository measureRepository,
            IMeasureViewModelMapper measureViewModelMapper,
            IValidator<Measure> measureValidator,
            IOrganizationQuery organizationQuery,
            IStringLocalizer<ExceptionResource> localizer)
        {
            _uowManager = uowManager;
            _measureRepository = measureRepository;
            _measureViewModelMapper = measureViewModelMapper;
            _measureValidator = measureValidator;
            _organizationQuery = organizationQuery;
            _localizer = localizer;
        }

        public async Task<MeasureViewModel> CreateAsync(MeasureViewModel viewModel)
        {
            var measure = _measureViewModelMapper.MapToDomainModel(viewModel);

            if (IsValid(measure))
            {
                using var uow = _uowManager.GetUnitOfWork();
                _measureRepository.Create(measure);
                await uow.SaveChangesAsync();
            }               

            var resultViewModel = _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel, measure);

            return resultViewModel;
        }

        public async Task<MeasureViewModel> UpdateAsync(int id, MeasureViewModel viewModel)
        {
            var update = _measureViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            var measure = await _measureRepository
                .GetByIdAsync(id);            

            measure.Update(update);

            if (IsValid(measure))
                await uow.SaveChangesAsync();

            var resultViewModel = _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
            await CompleteDataForViewModel(resultViewModel, measure);

            return resultViewModel;
        }

        public async Task DeleteAsync(int id)
        {
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

        private bool IsValid(Measure measure)
        {
            measure.ValidationResult = _measureValidator
                .Validate(measure);

            return measure.ValidationResult.IsValid;
        }
    }
}
