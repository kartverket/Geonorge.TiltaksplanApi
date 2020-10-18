using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IActionPlanQuery
    {
        Task<IList<ActionPlanViewModel>> GetAllAsync(string culture);
        Task<ActionPlanViewModel> GetByIdAsync(int id, string culture);
    }
}
