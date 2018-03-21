using LoggingLibrary.Entities.Layouts;
using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Appenders
{
    public class AppenderFactory
    {
        public static IAppender CreateAppender(string appenderType, ILayout layout)
        {
            if (appenderType.Equals("FileAppender"))
            {
                return new FileAppender(layout);
            }

            return new ConsoleAppender(layout);
        }
    }
}