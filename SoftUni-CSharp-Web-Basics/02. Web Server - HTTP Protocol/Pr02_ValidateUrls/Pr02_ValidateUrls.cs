namespace Pr02_ValidateUrls
{
    using System;
    using System.Net;

    public class Pr02_ValidateUrls
    {
        public static void Main()
        {
            var encodedUrl = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(encodedUrl);
            
            var hasValidHttps = decodedUrl.Contains("https://");
            
            var hasValidHttp = decodedUrl.Contains("http://");

            if (!hasValidHttp && !hasValidHttps)
            {
                Console.WriteLine("Invalid URL");
                Environment.Exit(0);
            }

           
            var parsedUrl = new Uri(decodedUrl);

            if (string.IsNullOrEmpty(parsedUrl.Scheme) || string.IsNullOrEmpty(parsedUrl.Host) ||
                string.IsNullOrEmpty(parsedUrl.Port.ToString()) || string.IsNullOrEmpty(parsedUrl.AbsolutePath)
            )
            {
                Console.WriteLine("Invalid URL");
            }
            else if (parsedUrl.Scheme.Equals("http") && parsedUrl.Port == 443 ||
                     parsedUrl.Scheme.Equals("https") && parsedUrl.Port == 80)
            {
                Console.WriteLine("Invalid URL");
            }
            else
            {
                Console.WriteLine($"Protocol: {parsedUrl.Scheme}");
                Console.WriteLine($"Host: {parsedUrl.Host}");
                Console.WriteLine($"Port: {parsedUrl.Port}");
                Console.WriteLine($"Path: {parsedUrl.AbsolutePath}");

                if (!string.IsNullOrEmpty(parsedUrl.Query) && !string.IsNullOrEmpty(parsedUrl.Fragment))
                {
                    Console.WriteLine($"Query: {parsedUrl.Query}");
                    Console.WriteLine($"Fragment: {parsedUrl.Fragment}");
                }
            }
        }
    }
}