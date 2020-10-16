namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel.UnitOfWork
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork GetUnitOfWork();
    }
}
