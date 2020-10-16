using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Domain.Repositories
{
    public interface IAsyncRepository<TDomainObject, in TId>
    {
        Task CreateAsync(TDomainObject domainObject);
        Task UpdateAsync(TId id, TDomainObject domainObject);
        Task DeleteAsync(TId id);
    }
}
