using Geonorge.TiltaksplanApi.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel
{
    public class DataSeeder
    {
        public static void SeedLanguages(MeasurePlanContext context)
        {
            if (context.Languages.Count() == 0)
            {
                var languages = new List<Language>
                {
                    new Language { Culture = "nb-NO", Name = "Norsk", },
                    new Language { Culture = "en-US", Name = "Engelsk",  }
                };

                context.AddRange(languages);
                context.SaveChanges();
            }
        }

        public static void SeedOrganizations(MeasurePlanContext context, string apiUrl)
        {
            if (context.Organizations.Count() == 0)
            {
                var organizations = GetOrganizations(apiUrl).Result;
                context.AddRange(organizations);
                context.SaveChanges();
            }
        }

        private static async Task<List<Organization>> GetOrganizations(string apiUrl)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseBody);

            return jObject["containeditems"]
                .Select(jToken => {
                    return new Organization
                    {
                        Name = jToken["label"].Value<string>(),
                        OrgNumber = jToken["number"]?.Value<long>()
                    };
                })
                .ToList();
        }
    }
}
