namespace Spacial.Core.Common
{
    public static class Extensions
    {
        public static long EnsureNeverNegative(this long source)
        {
            return source < 0 ? 0 : source;
        }

        public static int EnsureNeverNegative(this int source)
        {
            return source < 0 ? 0 : source;
        }

        public static string RemoveLastCharacter(this string source)
        {
            return source.Substring(0, source.Length - 1);
        }

        public static DateTime GetStartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
    }
}
