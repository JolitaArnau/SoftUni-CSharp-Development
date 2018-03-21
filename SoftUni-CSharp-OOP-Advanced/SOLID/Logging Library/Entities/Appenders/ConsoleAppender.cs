using System;
using System.Text;
using LoggingLibrary.Enums;
using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Appenders
{
    public class ConsoleAppender : IAppender
    {

        public ConsoleAppender(ILayout layout)
        {
            this.Layout = layout;
        }

        public ILayout Layout { get; }
        
        public ReportLevel ReportLevel { get; set; }
        
        public int Count { get; set; }

        public void Append(string date, string reportLevel, string message)
        {
            var formatedMessage = this.Layout.FormatMessage(date, reportLevel, message);
            Console.WriteLine(formatedMessage);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            return sb.Append($"Appender type: {this.GetType().Name}, ")
                .Append($"Layout type: {this.Layout.GetType().Name}, ")
                .Append($"Report level: {this.ReportLevel}, ")
                .Append($"Messages appended: ")
                .ToString();
        }
    }
}