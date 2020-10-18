using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public interface IActionPlanViewModelMapper
    {
        ActionPlan MapToDomainModel(ActionPlanViewModel viewModel);
        ActionPlanViewModel MapToViewModel(ActionPlan domainModel, string culture);
    }
}
