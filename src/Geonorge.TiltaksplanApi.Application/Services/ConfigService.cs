using Geonorge.TiltaksplanApi.Application.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IUrlProvider _urlProvider;
        
        private static readonly ResourceManager _resourceManager =
            new ResourceManager($"{Assembly.GetExecutingAssembly().GetName().Name}.resources.AppTextResource", typeof(AppTextResource).Assembly);

        private static readonly List<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("nb-NO", true),
            new CultureInfo("en-US", true)
        };

        public ConfigService(
            IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public ConfigViewModel Get()
        {
            return new ConfigViewModel
            {
                ApiUrls = _urlProvider.ApiUrls(),
                Translations = GetTranslations()
            };
        }

        private List<TranslationViewModel> GetTranslations()
        {
            return _supportedCultures
                .Select(culture =>
                {
                    var resourceSet = _resourceManager.GetResourceSet(culture, true, false);
                    var enumerator = resourceSet.GetEnumerator();
                    var texts = new Dictionary<string, string>();

                    while (enumerator.MoveNext())
                        texts.Add(enumerator.Key.ToString(), enumerator.Value.ToString());

                    return new TranslationViewModel
                    {
                        Culture = culture.Name,
                        Texts = texts
                    };
                })
                .ToList();
        }
    }
}
