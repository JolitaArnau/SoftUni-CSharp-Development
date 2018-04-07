using System;
using Twitter.Contracts;

namespace Twitter.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}