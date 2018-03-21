using System;

namespace LoggingLibrary.Interfaces
{
    public interface ILayout
    {
        string FormatMessage(string date, string reportLevel, string message);
    }
}