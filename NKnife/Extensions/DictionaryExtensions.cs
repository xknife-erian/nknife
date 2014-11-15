﻿using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 详细比较两个字典是否内容一致
        /// </summary>
        /// <param name="source">源字典</param>
        /// <param name="target">目标字典</param>
        /// <returns></returns>
        public static bool Compare<TK, TV>(this IDictionary<TK, TV> source, IDictionary<TK, TV> target)
        {
            if (source.Count != target.Count)
                return false;
            foreach (TK key in source.Keys)
            {
                if (!target.ContainsKey(key))
                    return false;
                if (!source[key].Equals(target[key]))
                    return false;
            }
            return true;
        }

        public static void Add<TK, TV>(this Dictionary<TK, TV> dictionary, KeyValuePair<TK, TV> pair)
        {
            dictionary.Add(pair.Key, pair.Value);
        }
    }
}