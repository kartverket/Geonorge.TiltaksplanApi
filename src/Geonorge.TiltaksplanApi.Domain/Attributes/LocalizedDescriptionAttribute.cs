using System;
using System.ComponentModel;
using System.Resources;

namespace Geonorge.TiltaksplanApi.Domain.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;

        public LocalizedDescriptionAttribute(
            string resourceKey)
        {
            _resource = new ResourceManager("Geonorge.TiltaksplanApi.Domain.Resources.EnumResource", typeof(EnumResource).Assembly);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }
}
