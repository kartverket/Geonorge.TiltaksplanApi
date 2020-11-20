﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Geonorge.TiltaksplanApi.Web.Configuration
{
    public class AuthorizeGeoID : Attribute, IAuthorizationFilter
    {
        private IConfiguration _configuration;
        private static readonly HttpClient _httpClient = new HttpClient();

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            SetConfig();

            if (filterContext != null)
            {
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    var authToken = _token.Replace("Bearer ", "");
                    if (authToken != null)
                    {
                        if (!IsValidToken(authToken))
                        {
                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid access token"
                                }
                            };
                        }
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please provide access token";
                    filterContext.Result = new JsonResult("Please provide access token")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please provide access token"
                        },
                    };
                }
            }
        }

        private void SetConfig()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        private bool IsValidToken(string authToken)
        {
            var geoIDConfig = _configuration.GetSection(GeoIDConfiguration.SectionName).Get<GeoIDConfiguration>();

            var byteArray = Encoding.ASCII.GetBytes(geoIDConfig.IntrospectionCredentials);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var formUrlEncodedContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("token", authToken) });
            using var result = _httpClient.PostAsync(geoIDConfig.IntrospectionUrl, formUrlEncodedContent).Result;

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