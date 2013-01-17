using System.Globalization;

namespace Journey.Test.Support.Model
{
    public static class Extensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}