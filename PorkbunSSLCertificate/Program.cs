using PorkbunSSLCertificate;
using PorkbunSSLCertificate.Models;

PorkbunAPI porkbunAPI = new(ConfigurationManager.Get("API:BaseAddress"),
                            ConfigurationManager.Get("API:ApiKey"),
                            ConfigurationManager.Get("API:SecretApiKey"));

try
{
    PorkbunApiResponse? apiResponse = await porkbunAPI.RetrieveSslCertificateBundle(ConfigurationManager.Get("Domain"));

    if (apiResponse is null)
    {
        throw new Exception("apiResponse is null.");
    }

    string pathCertificateChain = ConfigurationManager.Get("Path:CertificateChain");
    string pathIntermediateCertificate = ConfigurationManager.Get("Path:IntermediateCertificate");
    string pathPublicKey = ConfigurationManager.Get("Path:PublicKey");
    string pathPrivateKey = ConfigurationManager.Get("Path:PrivateKey");

    if (pathCertificateChain != string.Empty)
    {
        await File.WriteAllTextAsync(pathCertificateChain, apiResponse.CertificateChain);
        Console.WriteLine("File saved : " + Path.GetFullPath(pathCertificateChain));
    }
    if (pathIntermediateCertificate != string.Empty)
    {
        await File.WriteAllTextAsync(pathIntermediateCertificate, apiResponse.IntermediateCertificate);
        Console.WriteLine("File saved : " + Path.GetFullPath(pathIntermediateCertificate));
    }
    if (pathPublicKey != string.Empty)
    {
        await File.WriteAllTextAsync(pathPublicKey, apiResponse.PublicKey);
        Console.WriteLine("File saved : " + Path.GetFullPath(pathPublicKey));
    }
    if (pathPrivateKey != string.Empty)
    {
        await File.WriteAllTextAsync(pathPrivateKey, apiResponse.PrivateKey);
        Console.WriteLine("File saved : " + Path.GetFullPath(pathPrivateKey));
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}