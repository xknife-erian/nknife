using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace System.Collections
{
    public static class EnumerableExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
        {
            var dataTable = new DataTable();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
            {
                dataTable.Columns.Add(pd.Name, pd.PropertyType);
            }
            foreach (T item in enumerable)
            {
                var row = dataTable.NewRow();
                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
                {
                    row[pd.Name] = pd.GetValue(item);
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        /// <summary>
        /// Execute action on each item in enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        /// <summary>
        /// Converts an enumerable collection to an delimited string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string AsDelimited<T>(this IEnumerable<T> items, string delimiter)
        {
            return String.Join(delimiter, items.Select(item => item.ToString()).ToArray());
        }

        #region Conditional Checks

        /// <summary>
        /// Check for any nulls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static unsafe bool HasAnyNulls<T>(this IEnumerable<T> items) where T : class
        {
            return IsTrueForAny(items, t => t == null);
        }


        /// <summary>
        /// Check if any of the items in the collection satisfied by the condition.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        public static bool IsTrueForAny<T>(this IEnumerable<T> items, Func<T, bool> executor)
        {
            return items.Select(executor).Any(result => result);
        }


        /// <summary>
        /// Check if all of the items in the collection satisfied by the condition.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        public static bool IsTrueForAll<T>(this IEnumerable<T> items, Func<T, bool> executor)
        {
            return items.Select(executor).All(result => result);
        }


        /// <summary>
        /// Check if all of the items in the collection satisfied by the condition.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IDictionary<T, T> ToDictionary<T>(this IList<T> items)
        {
            IDictionary<T, T> dict = new Dictionary<T, T>();
            foreach (T item in items)
            {
                dict[item] = item;
            }
            return dict;
        }

        #endregion
    }
}
