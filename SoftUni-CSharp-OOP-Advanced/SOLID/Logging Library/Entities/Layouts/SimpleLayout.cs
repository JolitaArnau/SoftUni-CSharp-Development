using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string FormatMessage(string date, string reportLevel, string message)
        {
            return $"{date} - {reportLevel} - {message}";
        }
    }
}