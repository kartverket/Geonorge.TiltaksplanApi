using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWorkManager _uowManager;
        private readonly IActivityRepository _activityRepository;
        private readonly IActivityViewModelMapper _activityViewModelMapper;

        public ActivityService(
            IUnitOfWorkManager uowManager,
            IActivityRepository activityRepository,
            IActivityViewModelMapper activityViewModelMapper)
        {
            _uowManager = uowManager;
            _activityRepository = activityRepository;
            _activityViewModelMapper = activityViewModelMapper;
        }

        public async Task<ActivityViewModel> CreateAsync(ActivityViewModel viewModel)
        {
            var activity = _activityViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            _activityRepository.Create(activity);
            await uow.SaveChangesAsync();

            return _activityViewModelMapper.MapToViewModel(activity, viewModel.Culture);
        }

        public async Task<ActivityViewModel> UpdateAsync(int id, ActivityViewModel viewModel)
        {
            var update = _activityViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            var actionPlan = await _activityRepository
                .GetByIdAsync(id);

            actionPlan.Update(update);
            await uow.SaveChangesAsync();

            return _activityViewModelMapper.MapToViewModel(update, viewModel.Culture);
        }

        public async Task DeleteAsync(int id)
        {
            using var uow = _uowManager.GetUnitOfWork();
            var activity = await _activityRepository.GetByIdAsync(id);
            _activityRepository.Delete(activity);

            await uow.SaveChangesAsync();
        }
    }
}
