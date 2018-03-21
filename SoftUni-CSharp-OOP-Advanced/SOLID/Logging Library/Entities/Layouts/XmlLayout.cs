using System;
using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Layouts
{
    public class XmlLayout : ILayout
    {
        public string FormatMessage(string date, string reportLevel, string message)
        {
            return $"<log>{Environment.NewLine}" +
                   $"    <date>{date}</date>{Environment.NewLine}" +
                   $"    <level>{reportLevel}</level>{Environment.NewLine}" +
                   $"    <message>{message}</message>{Environment.NewLine}" +
                   $"</log>";
        }
    }
}