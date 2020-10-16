using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Geonorge.TiltaksplanApi.Application.Mapping;
using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ActionPlanService : IActionPlanService
    {
        private readonly IActionPlanRepository _actionPlanRepository;
        private readonly IViewModelMapper<ActionPlan, ActionPlanViewModel> _actionPlanViewModelMapper;

        public ActionPlanService(
            IActionPlanRepository actionPlanRepository,
            IViewModelMapper<ActionPlan, ActionPlanViewModel> actionPlanViewModelMapper)
        {
            _actionPlanRepository = actionPlanRepository;
            _actionPlanViewModelMapper = actionPlanViewModelMapper;
        }

        public async Task<List<ActionPlanViewModel>> GetAllAsync()
        {
            var actionPlans = await _actionPlanRepository
                .GetAll()
                .ToListAsync();

            return actionPlans
                .ConvertAll(actionPlan => _actionPlanViewModelMapper.MapToViewModel(actionPlan));
        }

        public async Task<ActionPlanViewModel> GetAsync(int id)
        {
            var actionPlan = await _actionPlanRepository
                .GetByIdAsync(id);

            return _actionPlanViewModelMapper.MapToViewModel(actionPlan);

        }
    }
}
