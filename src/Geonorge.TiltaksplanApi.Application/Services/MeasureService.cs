using FluentValidation;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IUnitOfWorkManager _uowManager;
        private readonly IMeasureRepository _measureRepository;
        private readonly IMeasureViewModelMapper _measureViewModelMapper;
        private readonly IValidator<Measure> _measureValidator;

        public MeasureService(
            IUnitOfWorkManager uowManager,
            IMeasureRepository measureRepository,
            IMeasureViewModelMapper measureViewModelMapper,
            IValidator<Measure> measureValidator)
        {
            _uowManager = uowManager;
            _measureRepository = measureRepository;
            _measureViewModelMapper = measureViewModelMapper;
            _measureValidator = measureValidator;
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

            return _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
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

            return _measureViewModelMapper.MapToViewModel(measure, viewModel.Culture);
        }

        public async Task DeleteAsync(int id)
        {
            using var uow = _uowManager.GetUnitOfWork();
            var measure = await _measureRepository.GetByIdAsync(id);
            _measureRepository.Delete(measure);

            await uow.SaveChangesAsync();
        }

        private bool IsValid(Measure measure)
        {
            measure.ValidationResult = _measureValidator
                .Validate(measure);

            return measure.ValidationResult.IsValid;
        }
    }
}
