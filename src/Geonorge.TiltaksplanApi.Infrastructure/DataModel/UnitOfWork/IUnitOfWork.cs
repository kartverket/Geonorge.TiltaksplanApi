using System;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        MeasurePlanContext Context { get; }
        bool IsRoot { get; }
        Task SaveChangesAsync();
    }
}
