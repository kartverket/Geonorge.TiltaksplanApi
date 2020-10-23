namespace Geonorge.TiltaksplanApi.Domain.Services.Validation
{
    public interface IValidationService<in TDomainModel>
    {
        bool Validate(TDomainModel domainModel);
    }
}
