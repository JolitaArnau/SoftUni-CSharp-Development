using LoggingLibrary.Enums;
using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Appenders
{
    public class FileAppender : IAppender
    {
        public FileAppender(ILayout layout)
        {
            this.Layout = layout;
        }
        
        public ILayout Layout { get; }
        
        public ReportLevel ReportLevel { get; set; }
        
        public LogFile File { get; set; }

        public void Append(string date, string reportLevel, string message)
        {
            var formatedMessage = this.Layout.FormatMessage(date, reportLevel, message);
            this.File.Write(formatedMessage);
        }
    }
}