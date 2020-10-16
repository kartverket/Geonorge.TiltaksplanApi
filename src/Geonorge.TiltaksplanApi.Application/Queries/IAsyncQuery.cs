using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Queries
{
    public interface IAsyncQuery<TModel>
    {
        Task<TModel> ExecuteAsync();
    }

    public interface IAsyncQuery<TModel, in TId>
    {
        Task<TModel> ExecuteAsync(TId id);
    }

    public interface IAsyncQuery<TModel, in TSkip, in TTake>
    {
        Task<TModel> ExecuteAsync(TSkip skip, TTake take);
    }
}
