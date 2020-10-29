using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public abstract class ViewModelWithValidation
    {
        [JsonIgnore]
        public List<string> ValidationErrors { get; set; }
        [JsonIgnore]
        public bool IsValid => ValidationErrors == null || !ValidationErrors.Any();
    }
}
