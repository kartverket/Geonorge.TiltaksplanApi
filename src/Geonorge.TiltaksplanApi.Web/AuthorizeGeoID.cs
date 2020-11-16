using Geonorge.TiltaksplanApi.Application.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Geonorge.TiltaksplanApi.Web
{
    public class AuthorizeGeoID : Attribute, IAuthorizationFilter
    {

        private static HttpClient _httpClient = new HttpClient();

        IConfigurationProvider _configuration;


        /// <summary>  
        /// Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            GetConfig();

            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    string authToken = _token.Replace("Bearer ", ""); ;
                    if (authToken != null)
                    {
                        if (IsValidToken(authToken))
                        {

                            return;
                        }
                        else
                        {

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid access token"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide access token";
                    filterContext.Result = new JsonResult("Please Provide access token")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide access token"
                        },
                    };
                }
            }
        }

        private void GetConfig()
        {
            IConfigurationRoot config;

            if (Debugger.IsAttached) 
            {
                 config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Development.json")
                .Build();
            }
            else 
            {
                config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            }

            _configuration = config.Providers.ElementAt(0);
        }

        public bool IsValidToken(string authToken)
        {
            string geoIdIntrospectionUrl;
            _configuration.TryGet("GeoID:IntrospectionUrl", out geoIdIntrospectionUrl);

            string geoIdIntrospectionCredentials;
            _configuration.TryGet("GeoID:IntrospectionCredentials", out geoIdIntrospectionCredentials);


            var byteArray = Encoding.ASCII.GetBytes(geoIdIntrospectionCredentials);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var formUrlEncodedContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("token", authToken) });
            HttpResponseMessage result = _httpClient.PostAsync(geoIdIntrospectionUrl, formUrlEncodedContent).Result;

            if (result.IsSuccessStatusCode)
            {
                var rawResponse = result.Content.ReadAsStringAsync().Result;


                var jsonResponse = JObject.Parse(rawResponse);
                if (jsonResponse["active"] != null)
                {
                    var isActiveToken = jsonResponse["active"].Value<bool>();
                    if (isActiveToken)
                        return true;
                }
            }

            return false;
        }
    }
}
