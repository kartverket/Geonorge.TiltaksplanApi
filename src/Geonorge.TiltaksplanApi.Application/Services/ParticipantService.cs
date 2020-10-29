using Geonorge.TiltaksplanApi.Application.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;

        public ParticipantService(
            IOptions<ApiUrlsConfiguration> options)
        {
            _apiUrlsConfiguration = options.Value;
        }

        public async Task<List<string>> GetAllAsync()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(_apiUrlsConfiguration.Organizations);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jObject = JsonConvert.DeserializeObject<JObject>(responseBody);

            return jObject["containeditems"]
                .Select(jToken => jToken["label"].Value<string>())
                .OrderBy(label => label)
                .ToList();
        }
    }
}
