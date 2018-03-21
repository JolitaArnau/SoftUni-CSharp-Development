using System;
using System.Text;
using LoggingLibrary.Enums;
using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities
{
    public class Logger : ILogger

    {
        private IAppender[] appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }

        private void Log(string date, string reportLevel, string message)
        {
            foreach (var appender in this.appenders)
            {
                ReportLevel currentReportLevel = (ReportLevel) Enum.Parse(typeof(ReportLevel), reportLevel);

                if (currentReportLevel >= appender.ReportLevel)
                {
                    appender.Append(date, reportLevel, message);
                }
            }
        }

        public void Error(string date, string message)
        {
            this.Log(date, "Error", message);
        }

        public void Info(string date, string message)
        {
            this.Log(date, "Info", message);
        }

        public void Fatal(string date, string message)
        {
            this.Log(date, "Fatal", message);
        }

        public void Critical(string date, string message)
        {
            this.Log(date, "Critical", message);
        }

        public void Warn(string date, string message)
        {
            this.Log(date, "Warning", message);
        }
    }
}