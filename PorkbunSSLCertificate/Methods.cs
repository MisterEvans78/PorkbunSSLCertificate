namespace PorkbunSSLCertificate
{
    public static class Methods
    {
        public static async Task<bool> SaveFileIfContentIsDifferentAsync(string path, string content)
        {
            if (File.Exists(path))
            {
                string fileContent = await File.ReadAllTextAsync(path);

                if (content == fileContent)
                {
                    Console.WriteLine("File already exists: " + path);
                    return false;
                }
            }
            
            await File.WriteAllTextAsync(path, content);
            Console.WriteLine("File saved: " + path);
            return true;
        }
    }
}
