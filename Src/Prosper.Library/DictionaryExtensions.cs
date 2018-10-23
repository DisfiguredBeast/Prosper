using Prosper.Library;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static TI Get<TK, TI>(this Dictionary<TK, TI> dictionary, TK key)
        {
            Guard.NotNull(dictionary, nameof(dictionary));
            Guard.NotNull(key, nameof(key));

            TI item;
            if (!dictionary.TryGetValue(key, out item))
                item = default(TI);

            return item;
        }

        public static TI GetOrAdd<TK, TI>(this Dictionary<TK, TI> dictionary, TK key, Func<TI> creator)
        {
            Guard.NotNull(dictionary, nameof(dictionary));
            Guard.NotNull(key, nameof(key));
            Guard.NotNull(creator, nameof(creator));

            if (dictionary.TryGetValue(key, out TI item))
                return item;

            item = creator();
            dictionary.Add(key, item);
            return item;
        }
    }
}
