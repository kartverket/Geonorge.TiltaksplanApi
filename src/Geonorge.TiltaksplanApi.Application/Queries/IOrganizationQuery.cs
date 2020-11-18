using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IOrganizationQuery
    {
        Task<IList<OrganizationViewModel>> GetAllAsync();
        Task<OrganizationViewModel> GetByIdAsync(int id);
    }
}
