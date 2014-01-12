using System.Collections.Generic;

namespace NKnife.Extensions
{
    public static class DictionaryEx
    {
        public static void Add<TK, TV>(this Dictionary<TK, TV> dictionary, KeyValuePair<TK, TV> pair)
        {
            dictionary.Add(pair.Key, pair.Value);
        }
    }
}