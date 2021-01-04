using Geonorge.TiltaksplanApi.Application.Models;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID
{
    public interface IGeoIDService
    {
        Task<UserViewModel> GetUser();
    }
}
