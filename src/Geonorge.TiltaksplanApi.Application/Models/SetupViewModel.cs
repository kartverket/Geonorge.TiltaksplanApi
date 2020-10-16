using System.Dynamic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class SetupViewModel
    {
        public string Environment { get; set; }
        public ExpandoObject ApiUrls { get; set; }
    }
}
