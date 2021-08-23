using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NKnife.ShareResources;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class NullExtension
    {
        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            if (data == null) return true;
                
            if (data is string str)
            {
                return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
            }
            //如果为DBNull
            if (data is DBNull)
                return true;

            //不为空
            return false;
        }

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(this object data)
        {
            return IsNullOrEmpty<object>(data);
        }

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            if (data == null || !data.Any())
                return true;
            return false;
        }

    }
}
