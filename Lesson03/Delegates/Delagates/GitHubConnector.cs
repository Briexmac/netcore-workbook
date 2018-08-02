using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Delagates
{
    public class GitHubConnector
    {
        private readonly HttpClient _httpClient;

        public GitHubConnector(string userName)
        {
            var accessToken = Environment.GetEnvironmentVariable("MyACAGitHubToken", EnvironmentVariableTarget.User);
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("ACA-NET-Core")));
            _httpClient.BaseAddress = new Uri($"https://api.github.com/users/{userName}");
        }

        public async Task<string> GetUserInfoAsync()
        {
            var response = await _httpClient.GetAsync("").ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<string> GetFormattedUserInfoAsync()
        {
            var body = await GetUserInfoAsync().ConfigureAwait(false);
            var token = JsonConvert.DeserializeObject<JToken>(body);
            return token.ToString(Formatting.Indented);
        }
    }
}
