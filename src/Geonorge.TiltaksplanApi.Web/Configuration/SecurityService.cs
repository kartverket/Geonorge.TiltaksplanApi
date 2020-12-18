using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Geonorge.TiltaksplanApi.Web.Configuration
{
    class SecurityService : ISecurityService
    {
        private IConfiguration _configuration;
        private static readonly HttpClient _httpClient = new HttpClient();

        public UserViewModel GetUserInfo(HttpContext httpContext)
        {
            SetConfig();
            httpContext.Request.Headers.TryGetValue("Authorization", out var authTokens);

            var _token = authTokens.FirstOrDefault();

            if (_token != null)
            {
                var authToken = _token.Replace("Bearer ", "");
                if (authToken != null)
                    return GetUserInfoByToken(authToken);
            }

            return new UserViewModel();
        }

        private void SetConfig()
        {
            if (Debugger.IsAttached)
            {
                _configuration = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.Development.json")
               .Build();
            }
            else
            {
                _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            }
        }

        private UserViewModel GetUserInfoByToken(string authToken)
            {
            UserViewModel userViewModel = new UserViewModel();
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
                        {
                            if (jsonResponse["username"] != null)
                            {
                                var username = jsonResponse["username"].Value<string>();
                                if (!string.IsNullOrWhiteSpace(username))
                                {
                                    if (username.Contains('@'))
                                        username = username.Split('@')[0];
                                }
                            GetOrganizationInfo(username, ref userViewModel);
                            userViewModel.Roles = GetRoles(username, authToken);
                            }
                        }
                    }
                }

            return userViewModel;
            }

        private void GetOrganizationInfo(string username, ref UserViewModel userViewModel)
        {
            var geoIDConfig = _configuration.GetSection(GeoIDConfiguration.SectionName).Get<GeoIDConfiguration>();
            var geoIdUserInfoUrl = geoIDConfig.BaatAuthzApiUrl + "authzinfo/" + username;

            var byteArray = Encoding.ASCII.GetBytes(geoIDConfig.BaatAuthzApiCredentials);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage result = _httpClient.GetAsync(geoIdUserInfoUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                var rawResponse = result.Content.ReadAsStringAsync().Result;

                var jsonResponse = JObject.Parse(rawResponse);
                var organization = jsonResponse["organization"];
                if (organization != null) 
                {
                    userViewModel.OrganizationName = organization["name"].ToString();
                    userViewModel.OrganizationNumber = organization["orgnr"].ToString();
                }
            }
        }

        private List<string> GetRoles(string username, string accessToken)
        {
            var geoIDConfig = _configuration.GetSection(GeoIDConfiguration.SectionName).Get<GeoIDConfiguration>();

            var geoIdUserInfoUrl = geoIDConfig.BaatAuthzApiUrl + "info/" + username;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage result = _httpClient.GetAsync(geoIdUserInfoUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                var rawResponse = result.Content.ReadAsStringAsync().Result;

                var jsonResponse = JObject.Parse(rawResponse);
                if (jsonResponse["baat_services"] != null)
                {
                    var userRoles = jsonResponse["baat_services"].ToObject<List<string>>();
                    return userRoles;
                }
            }
            return new List<string>();
        }

    }
 }

