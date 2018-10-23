using Prosper.Library;

namespace System
{
    public static class StringExtensions
    {
        public static string FormatWith(this string s, params object[] args)
        {
            Guard.NotNull(s, nameof(s));

            return string.Format(s, args);
        }
    }
}
