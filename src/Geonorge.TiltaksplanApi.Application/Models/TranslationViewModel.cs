using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class TranslationViewModel
    {
        public string Culture { get; set; }
        public IDictionary<string, string> Texts { get; set; }
    }
}
