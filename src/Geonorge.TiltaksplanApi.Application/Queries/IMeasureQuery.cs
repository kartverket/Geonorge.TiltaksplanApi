using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IMeasureQuery
    {
        Task<IList<MeasureViewModel>> GetAllAsync(string culture, string organization);
        Task<MeasureViewModel> GetByIdAsync(int id, string culture);
        Task<MeasureViewModel> GetByNumberAsync(int number, string culture);
        Task<bool> HasOwnership(int id, long orgNumber);
        Task<bool> IsNumberAvailable(int id, int number);
    }
}
