using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class RandomExtension
    {
        private static readonly Random _Random;
        static RandomExtension()
        {
            _Random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// 返回一个小于或等于maxValue的随机正整数
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GetRandomItem(this int maxValue)
        {
            return _Random.Next(maxValue) + 1;
        }

        /// <summary>
        /// 从集合中随机选取一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="removeSelected">选后是否删除选中的，默认不删除</param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this List<T> list,bool removeSelected = false)
        {
            var count = list.Count;
            if(count == 0) return default(T);
            var index = _Random.Next(count);
            if (removeSelected)
            {
                T result = list[index];
                list.RemoveAt(index);
                return result;
            }
            return list[index];
        }

        public static T GetRandomItem<T>(this T[] array)
        {
            var count = array.Length;
            if (count == 0) return default(T);
            var index = _Random.Next(count);
            return array[index];
        }

        //public static T GetRandomItem<T>(this T array) where T:Enum
        //{
        //    var count = array.Length;
        //    if (count == 0) return default(T);
        //    var index = new Random().Next(count);
        //    return array[index];
        //} 

        public static TKey GetRandomKey<TKey,TValue>(this Dictionary<TKey,TValue> dictionary)
        {
            var count = dictionary.Count;
            if (count == 0) return default(TKey);
            var index = _Random.Next(count);
            return dictionary.Keys.ToArray()[index];
        }

        public static TKey GetRandomKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var count = dictionary.Count;
            if (count == 0) return default(TKey);
            var index = _Random.Next(count);
            return dictionary.Keys.ToArray()[index];
        }

        public static TValue GetRandomValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            var count = dictionary.Count;
            if (count == 0) return default(TValue);
            var index = _Random.Next(count);
            return dictionary.Values.ToArray()[index];
        }

        public static TValue GetRandomValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var count = dictionary.Count;
            if (count == 0) return default(TValue);
            var index = _Random.Next(count);
            return dictionary.Values.ToArray()[index];
        }
    }
}
