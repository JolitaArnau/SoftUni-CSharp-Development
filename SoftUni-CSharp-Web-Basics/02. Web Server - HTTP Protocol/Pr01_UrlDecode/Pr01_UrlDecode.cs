namespace HTTP_Protocol
{
    using System;
    using System.Net;

    public class Pr01_UrlDecode
    {
        public static void Main()
        {
            var encodedUrl = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(encodedUrl);

            Console.WriteLine(decodedUrl);
        }
    }
}