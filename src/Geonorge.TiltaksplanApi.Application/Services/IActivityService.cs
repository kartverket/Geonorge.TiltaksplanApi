using Geonorge.TiltaksplanApi.Application.Models;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IActivityService
    {
        Task<ActivityViewModel> CreateAsync(ActivityViewModel viewModel);
        Task<ActivityViewModel> UpdateAsync(int id, ActivityViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
