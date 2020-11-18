using Geonorge.TiltaksplanApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static IEnumerable<SelectOption> EnumToSelectOptions<T>() where T : Enum
        {
            var enumType = typeof(T);
            var dictionary = new Dictionary<int, string>();

            return Enum.GetValues(enumType).Cast<T>()
                .Select(value =>
                {
                    return new SelectOption
                    {
                        Value = Convert.ToInt32(value),
                        Label = value.GetDescription()
                    };
                });
        }

        public static IEnumerable<T> EnumToList<T>() where T : Enum
        {
            var enumType = typeof(T);
            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);

            enumValList.AddRange(from int value in enumValArray select (T)Enum.Parse(enumType, value.ToString()));
            enumValList.Sort();

            return enumValList;
        }
    }
}
