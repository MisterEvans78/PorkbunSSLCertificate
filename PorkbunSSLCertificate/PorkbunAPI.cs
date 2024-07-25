using PorkbunSSLCertificate.Models;
using System.Net.Http.Json;

namespace PorkbunSSLCertificate
{
    public class PorkbunAPI
    {
        private HttpClient _client;
        private string _apiKey;
        private string _secretApiKey;

        public PorkbunAPI(string baseAddress, string apiKey, string secretApiKey)
        {
            _client = new()
            {
                BaseAddress = new Uri(baseAddress),
            };

            _apiKey = apiKey;
            _secretApiKey = secretApiKey;
        }

        public async Task<PorkbunApiResponse?> RetrieveSslCertificateBundle(string domain)
        {
            PorkbunApiBody body = new()
            {
                ApiKey = _apiKey,
                SecretApiKey = _secretApiKey
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("ssl/retrieve/" + domain, body);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{(int)response.StatusCode} {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}");
            }
            
            return await response.Content.ReadFromJsonAsync<PorkbunApiResponse>();
        }
    }
}
