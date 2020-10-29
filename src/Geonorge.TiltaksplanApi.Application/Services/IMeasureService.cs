using Geonorge.TiltaksplanApi.Application.Models;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IMeasureService
    {
        Task<MeasureViewModel> CreateAsync(MeasureViewModel viewModel);
        Task<MeasureViewModel> UpdateAsync(int id, MeasureViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
