using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public interface IActivityViewModelMapper
    {
        Activity MapToDomainModel(ActivityViewModel viewModel);
        ActivityViewModel MapToViewModel(Activity domainModel, string culture);
    }
}
