using LoggingLibrary.Interfaces;

namespace LoggingLibrary.Entities.Layouts
{
    public class LayoutFactory
    {
        public static ILayout CreateLayout(string layoutType)
        {
            if (layoutType.Equals("XmlLayout"))
            {
                return new XmlLayout();
            }

            return new SimpleLayout();
        }
    }
}