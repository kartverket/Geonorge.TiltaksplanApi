using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IActionPlanService
    {
        public Task<List<ActionPlanViewModel>> GetAllAsync();
        public Task<ActionPlanViewModel> GetAsync(int id);
    }
}
