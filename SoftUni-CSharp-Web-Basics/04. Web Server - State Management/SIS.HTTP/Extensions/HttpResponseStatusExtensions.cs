using System;

namespace SIS.HTTP.Extensions
{
    using Enums;
    
    public static class HttpResponseStatusExtensions
    {
        public static string GetResponseLine(this HttpResponseStatusCode statusCode)
        {
            var isValidStatusCode = Enum.TryParse(statusCode.ToString(), out HttpResponseStatusCode responseStatusCode);

            if (!isValidStatusCode)
            {
                Console.WriteLine("error");
            }

            return $"{(int) responseStatusCode} {responseStatusCode}";
        }
    }
}