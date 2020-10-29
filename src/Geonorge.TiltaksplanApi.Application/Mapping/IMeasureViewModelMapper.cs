using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public interface IMeasureViewModelMapper
    {
        Measure MapToDomainModel(MeasureViewModel viewModel);
        MeasureViewModel MapToViewModel(Measure domainModel, string culture);
    }
}
