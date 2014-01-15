using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NKnife.Utility
{
    public class UtilityCollection
    {

        /// <summary>
        /// Runs an action for all elements in the input.
        /// </summary>
        public static void ForEach<T>(IEnumerable<T> input, Action<T> action)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            foreach (T element in input)
            {
                action(element);
            }
        }

        /// <summary>
        /// Adds all 
        /// <paramref name="elements"/> to <paramref name="list"/>.
        /// </summary>
        public static void AddRange<T>(ICollection<T> list, IEnumerable<T> elements)
        {
            foreach (T o in elements)
                list.Add(o);
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(T[] arr)
        {
            return Array.AsReadOnly(arr);
        }

        /// <summary>指示指定的数组是 null 或者 数组为空。
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified objects]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(object[] objects)
        {
            if (objects == null)
                return true;
            if (objects.Length <= 0)
                return true;
            return false;
        }

        /// <summary>指示指定的数组是 null 或者 数组为空。
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified objects]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(T[] objects)
        {
            if (objects == null)
                return true;
            if (objects.Length <= 0)
                return true;
            return false;
        }
    }
}
