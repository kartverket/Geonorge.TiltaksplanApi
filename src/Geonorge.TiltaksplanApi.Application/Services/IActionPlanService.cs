using Geonorge.TiltaksplanApi.Application.Models;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IActionPlanService
    {
        Task<ActionPlanViewModel> CreateAsync(ActionPlanViewModel viewModel);
        Task<ActionPlanViewModel> UpdateAsync(int id, ActionPlanViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
