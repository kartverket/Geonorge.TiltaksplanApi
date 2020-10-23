using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using Geonorge.TiltaksplanApi.Domain.Services.Validation;
using Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ActionPlanService : IActionPlanService
    {
        private readonly IUnitOfWorkManager _uowManager;
        private readonly IActionPlanRepository _actionPlanRepository;
        private readonly IActionPlanViewModelMapper _actionPlanViewModelMapper;
        private readonly IValidationService<ActionPlan> _actionPlanValidationService;

        public ActionPlanService(
            IUnitOfWorkManager uowManager,
            IActionPlanRepository actionPlanRepository,
            IActionPlanViewModelMapper actionPlanViewModelMapper,
            IValidationService<ActionPlan> actionPlanValidationService)
        {
            _uowManager = uowManager;
            _actionPlanRepository = actionPlanRepository;
            _actionPlanViewModelMapper = actionPlanViewModelMapper;
            _actionPlanValidationService = actionPlanValidationService;
        }

        public async Task<ActionPlanViewModel> CreateAsync(ActionPlanViewModel viewModel)
        {
            var actionPlan = _actionPlanViewModelMapper.MapToDomainModel(viewModel);

            if (_actionPlanValidationService.Validate(actionPlan))
            {
                using var uow = _uowManager.GetUnitOfWork();
                _actionPlanRepository.Create(actionPlan);
                await uow.SaveChangesAsync();
            }               

            return _actionPlanViewModelMapper.MapToViewModel(actionPlan, viewModel.Culture);
        }

        public async Task<ActionPlanViewModel> UpdateAsync(int id, ActionPlanViewModel viewModel)
        {
            var update = _actionPlanViewModelMapper.MapToDomainModel(viewModel);

            using var uow = _uowManager.GetUnitOfWork();
            var actionPlan = await _actionPlanRepository
                .GetByIdAsync(id);

            actionPlan.Update(update);
            await uow.SaveChangesAsync();

            return _actionPlanViewModelMapper.MapToViewModel(update, viewModel.Culture);
        }

        public async Task DeleteAsync(int id)
        {
            using var uow = _uowManager.GetUnitOfWork();
            var actionPlan = await _actionPlanRepository.GetByIdAsync(id);
            _actionPlanRepository.Delete(actionPlan);

            await uow.SaveChangesAsync();
        }
    }
}
