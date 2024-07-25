using System.Text.Json.Serialization;

namespace PorkbunSSLCertificate.Models
{
    public class PorkbunApiResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("intermediatecertificate")]
        public string? IntermediateCertificate { get; set; }
        [JsonPropertyName("certificatechain")]
        public string? CertificateChain { get; set; }
        [JsonPropertyName("privatekey")]
        public string? PrivateKey { get; set; }
        [JsonPropertyName("publickey")]
        public string? PublicKey { get; set; }
    }
}
