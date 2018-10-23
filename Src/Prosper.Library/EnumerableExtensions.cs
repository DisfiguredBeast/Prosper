using Prosper.Library;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            Guard.NotNull(action, nameof(action));

            foreach (var item in enumerable)
                action(item);
        }
    }
}
