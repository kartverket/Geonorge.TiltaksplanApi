using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Domain.Repositories
{
    public interface IRepository<TDomainObject, in TId>
    {
        IQueryable<TDomainObject> GetAll();
        Task<TDomainObject> GetByIdAsync(TId id);
        TDomainObject Create(TDomainObject domainObject);
        void Delete(TDomainObject domainObject);
    }
}
