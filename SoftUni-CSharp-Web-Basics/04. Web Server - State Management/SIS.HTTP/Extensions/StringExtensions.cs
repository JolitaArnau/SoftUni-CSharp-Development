namespace SIS.HTTP.Extensions
{
    using System.Globalization;
    
    public static class StringExtensions
    {
        public static string Capitalize(string mixedCaseString)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return mixedCaseString = textInfo.ToTitleCase(mixedCaseString);
        }
    }
}