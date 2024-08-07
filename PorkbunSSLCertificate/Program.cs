﻿using PorkbunSSLCertificate;
using PorkbunSSLCertificate.Models;
using System.Diagnostics;

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

    bool fileEdited = false;
    string pathCertificateChain = ConfigurationManager.Get("Path:CertificateChain");
    string pathIntermediateCertificate = ConfigurationManager.Get("Path:IntermediateCertificate");
    string pathPublicKey = ConfigurationManager.Get("Path:PublicKey");
    string pathPrivateKey = ConfigurationManager.Get("Path:PrivateKey");

    if (pathCertificateChain != string.Empty && apiResponse.CertificateChain != null && apiResponse.CertificateChain.StartsWith("-----BEGIN"))
    {
        fileEdited = fileEdited | await Methods.SaveFileIfContentIsDifferentAsync(pathCertificateChain, apiResponse.CertificateChain);
    }
    if (pathIntermediateCertificate != string.Empty && apiResponse.IntermediateCertificate != null && apiResponse.IntermediateCertificate.StartsWith("-----BEGIN"))
    {
        fileEdited = fileEdited | await Methods.SaveFileIfContentIsDifferentAsync(pathIntermediateCertificate, apiResponse.IntermediateCertificate);
    }
    if (pathPublicKey != string.Empty && apiResponse.PublicKey != null && apiResponse.PublicKey.StartsWith("-----BEGIN"))
    {
        fileEdited = fileEdited | await Methods.SaveFileIfContentIsDifferentAsync(pathPublicKey, apiResponse.PublicKey);
    }
    if (pathPrivateKey != string.Empty && apiResponse.PrivateKey != null && apiResponse.PrivateKey.StartsWith("-----BEGIN"))
    {
        fileEdited = fileEdited | await Methods.SaveFileIfContentIsDifferentAsync(pathPrivateKey, apiResponse.PrivateKey);
    }

    if (fileEdited)
    {
        string processFileName = ConfigurationManager.Get("ProcessToReloadWebServer:FileName");
        string processArguments = ConfigurationManager.Get("ProcessToReloadWebServer:Arguments");

        if (processFileName != string.Empty)
        {
            ProcessStartInfo process = new() { FileName = processFileName, Arguments = processArguments };
            Process.Start(process);
            Console.WriteLine("Web server reloaded!");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}