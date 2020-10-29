using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public interface IParticipantService
    {
        Task<List<string>> GetAllAsync();
    }
}
