using LoggingLibrary.Enums;

namespace LoggingLibrary.Interfaces
{
    public interface IAppender
    {
        ILayout Layout { get; }

        ReportLevel ReportLevel { get; set; }

        void Append(string date, string reportLevel, string message);
    }
}