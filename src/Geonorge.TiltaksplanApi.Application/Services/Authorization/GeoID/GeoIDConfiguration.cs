﻿namespace Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID
{
    public class GeoIDConfiguration
    {
        public static string SectionName => "GeoID";
        public string IntrospectionUrl { get; set; }
        public string IntrospectionCredentials { get; set; }
        public string BaatAuthzApiUrl { get; set; }
        public string BaatAuthzApiCredentials { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
