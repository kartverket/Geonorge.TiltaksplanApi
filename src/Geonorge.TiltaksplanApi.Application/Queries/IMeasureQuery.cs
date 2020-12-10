using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IMeasureQuery
    {
        Task<IList<MeasureViewModel>> GetAllAsync(string culture);
        Task<MeasureViewModel> GetByIdAsync(int id, string culture);
    }
}
