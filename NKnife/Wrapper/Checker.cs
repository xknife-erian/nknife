﻿using System;
using System.Collections.Generic;
using System.Text;
using Gean.Resources;

namespace Gean
{
    public class Checker
    {

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null) return true;
                
            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                    return true;
                else
                    return false;
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
                return true;

            //不为空
            return false;
        }

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            return IsNullOrEmpty<object>(data);
        }

        /*以下Check方法来自Enterprise Library2005的Common项目*/

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否为空字符串。</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <remarks>
        /// <para>Before checking the <paramref name="variable"/>, a call is made to <see cref="ArgumentValidation.CheckForNullReference"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not be <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not be <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <pararef name="variable"/> can not be a zero length <see cref="string"/>.
        /// </exception>
        public static void CheckForEmptyString(string variable, string variableName)
        {
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");
            if (variable.Length == 0)
            {
                string message = string.Format(ArgumentValidationString.ExceptionEmptyString, variableName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否为空引用(Null)。</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException("variableName");
            }

            if (null == variable)
            {
                throw new ArgumentNullException(variableName);
            }
        }

        /// <summary>
        /// 验证输入的参数messageName非空字符串，也非空引用
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="messageName">Parameter value</param>
        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((null == name) || (name.Length == 0))
            {
                string message = string.Format(ArgumentValidationString.ExceptionInvalidNullNameArgument, messageName);
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// <para>验证参数<paramref name="bytes"/>非零长度，如果为零长度，则抛出异常<see cref="ArgumentException"/>。</para>
        /// </summary>
        /// <param name="bytes">
        /// The <see cref="byte"/> array to check.
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="bytes"/> can not be zero length.</para>
        /// </exception>
        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");
            if (bytes.Length == 0)
            {
                string message = string.Format(ArgumentValidationString.ExceptionByteArrayValueMustBeGreaterThanZeroBytes, variableName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// <para>检查参数<paramref name="variable"/>是否符合指定的类型。</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="type">
        /// <para>The <see cref="Type"/> expected type of <paramref name="variable"/>.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="typeName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="variable"/> is not the expected <see cref="Type"/>.
        /// </exception>
        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");
            if (!type.IsAssignableFrom(variable.GetType()))
            {
                string message = string.Format(ArgumentValidationString.ExceptionExpectedType, type.FullName);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// <para>Check <paramref name="variable"/> to determine if it is a valid defined enumeration for <paramref name="enumType"/>.</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="enumType">
        /// <para>The <see cref="Type"/> expected type of <paramref name="variable"/>.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="enumType"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="variable"/> is not the expected <see cref="Type"/>.
        /// <para>- or -</para>
        /// <par><paramref name="enumType"/> is not an <see cref="Enum"/>. </par>
        /// </exception>
        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");

            if (!Enum.IsDefined(enumType, variable))
            {
                string message = string.Format(ArgumentValidationString.ExceptionEnumerationNotDefined,
                    variable.ToString(), enumType.FullName, variableName);
                throw new ArgumentException(message);
            }
        }


        ///// <summary>
        ///// 判断某值是否在枚举内（位枚举）
        ///// </summary>
        ///// <param name="checkingValue">被检测的枚举值</param>
        ///// <param name="expectedValue">期望的枚举值</param>
        ///// <returns></returns>
        //public static bool Exists<E>(E checkingValue, E expectedValue) where E : Enum
        //{
        //    int intCheckingValue = Convert.ToInt32(checkingValue);
        //    int intExpectedValue = Convert.ToInt32(expectedValue);
        //    return (intCheckingValue & intExpectedValue) == intExpectedValue;
        //}
    }
}
