using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class DictionaryEx
    {
        public static void Add<TK, TV>(this Dictionary<TK, TV> dictionary, KeyValuePair<TK, TV> pair)
        {
            dictionary.Add(pair.Key, pair.Value);
        }
    }
}