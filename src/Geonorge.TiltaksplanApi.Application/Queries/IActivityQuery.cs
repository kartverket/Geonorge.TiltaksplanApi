using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IActivityQuery
    {
        Task<IList<ActivityViewModel>> GetAllAsync(string culture);
        Task<ActivityViewModel> GetByIdAsync(int id, string culture);
        Task<ActivityViewModel> GetByNumberAsync(int measureNumber, int number, string culture);
        Task<List<ActivityViewModel>> GetAllByMeasureNumberAsync(int measureNumber, string culture);
        Task<bool> IsNumberAvailable(int measureNumber, int id, int number);
    }
}
