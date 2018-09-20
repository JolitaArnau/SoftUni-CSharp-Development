namespace Pr03_RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    public class Program
    {
        public static void Main()
        {
            var line = Console.ReadLine();

            var pathsAndMethods = new HashSet<string>();
            
            while (!line.Equals("END"))
            {
                var pathAndMethod = line;

                pathsAndMethods.Add(pathAndMethod);

                line = Console.ReadLine();
            }

            var requestLine = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

            var method = requestLine[0].ToLower();
            var path = requestLine[1];

            foreach (var pathMethod in pathsAndMethods)
            {
                if (!pathMethod.Contains(path) && !pathMethod.Contains(method))
                {
                    Console.WriteLine("HTTP/1.1 404 NotFound" + Environment.NewLine +
                    "Content-Length: 9" + Environment.NewLine +
                    "Content-Type: text/plain" + Environment.NewLine +
                    "NotFound");
                }
                
                if (pathMethod.Contains(path) && pathMethod.Contains(method))
                {
                    Console.WriteLine("HTTP/1.1 200 OK" + Environment.NewLine +
                    "Content-Length: 2" + Environment.NewLine +
                    "Content-Type: text/plain" + Environment.NewLine +
                    "OK");
                }
            }            
        }
    }
}