namespace Geonorge.TiltaksplanApi.Web.Configuration
{
    public class GeoIDConfiguration
    {
        public static string SectionName => "GeoID";
        public string IntrospectionUrl { get; set; }
        public string IntrospectionCredentials { get; set; }
        public string BaatAuthzApiUrl { get; set; }
        public string BaatAuthzApiCredentials { get; set; }
    }
}
