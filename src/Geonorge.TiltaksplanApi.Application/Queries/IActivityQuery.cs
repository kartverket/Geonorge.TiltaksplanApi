using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IActivityQuery
    {
        Task<IList<ActivityViewModel>> GetAllAsync(string culture);
        Task<ActivityViewModel> GetByIdAsync(int id, string culture);
        Task<List<ActivityViewModel>> GetByMeasureIdAsync(int measureId, string culture);
    }
}
