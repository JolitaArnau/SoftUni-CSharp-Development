using System;
using System.IO;
using System.Linq;
using System.Text;

namespace LoggingLibrary.Entities
{
    public class LogFile
    {
        private const string FilePath = "log.txt";
        private StringBuilder stringBuilder;

        public LogFile()
        {
            this.stringBuilder = new StringBuilder();
        }

        public int Size { get; private set; }

        private int GetLettersSum(string message)
        {
            return message.Where(char.IsLetter).Sum(c => c);
        }

        public void Write(string message)
        {
            this.stringBuilder.AppendLine(message);
            File.AppendAllText(FilePath, Environment.NewLine + message + Environment.NewLine + "==========================================");
            this.Size = this.GetLettersSum(message);
        }
    }
}