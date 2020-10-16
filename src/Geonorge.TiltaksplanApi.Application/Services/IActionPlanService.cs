using Geonorge.TiltaksplanApi.Application.Models;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IActionPlanService
    {
        Task<ActionPlanViewModel> CreateActionPlan(ActionPlanViewModel viewModel);
        Task<ActionPlanViewModel> UpdateActionPlan(int id, ActionPlanViewModel viewModel);
        Task DeleteActionPlan(int id);
    }
}
