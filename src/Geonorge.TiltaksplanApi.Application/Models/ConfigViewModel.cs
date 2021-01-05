using System.Collections.Generic;
using System.Dynamic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class ConfigViewModel
    {
        public ExpandoObject ApiUrls { get; set; }
        public List<TranslationViewModel> Translations { get; set; }
    }
}
