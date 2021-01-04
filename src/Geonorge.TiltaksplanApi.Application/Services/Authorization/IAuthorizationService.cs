using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task AuthorizeActivity(UserActivity activity, int? entityId = null);
    }
}
