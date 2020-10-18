using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Models;

namespace Geonorge.TiltaksplanApi.Application.Mapping
{
    public class LanguageViewModelMapper : IViewModelMapper<Language, LanguageViewModel>
    {
        public Language MapToDomainModel(LanguageViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Language
            {
                Culture = viewModel.Culture,
                Name = viewModel.Name
            };
        }

        public LanguageViewModel MapToViewModel(Language domainModel)
        {
            if (domainModel == null)
                return null;

            return new LanguageViewModel
            {
                Culture = domainModel.Culture,
                Name = domainModel.Name
            };
        }
    }
}
