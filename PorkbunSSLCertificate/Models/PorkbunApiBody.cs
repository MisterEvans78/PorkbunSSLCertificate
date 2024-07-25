using System.Text.Json.Serialization;

namespace PorkbunSSLCertificate.Models
{
    public class PorkbunApiBody
    {
        [JsonPropertyName("apikey")]
        public required string ApiKey { get; set; }
        [JsonPropertyName("secretapikey")]
        public required string SecretApiKey { get; set; }
    }
}
