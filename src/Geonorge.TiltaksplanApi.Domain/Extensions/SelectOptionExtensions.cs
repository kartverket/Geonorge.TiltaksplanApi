using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Extensions
{
    public static class SelectOptionExtensions
    {
        public static IEnumerable<SelectOption> ToSelectOptions(this int[] values)
        {
            return values
                .ToList()
                .Select(value =>
                {
                    return new SelectOption
                    {
                        Value = value,
                        Label = value.ToString()
                    };
                });
        }
    }
}
