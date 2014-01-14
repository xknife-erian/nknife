using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringEx
    {
        /// <summary>是否是Null,空,全部是空白或全部为0的字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>
        ///   <c>true</c> if [is composed by zero] [the specified STR]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmptyAndZero(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            int n = str.Length;
            string mStr = "[0]{" + n.ToString() + ",}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>是否是拉丁字母（大小写均可）
        /// </summary>
        public static bool IslLatinLetter(this char c) //
        {
            const string patten = "^[A-Za-z]+$";
            var r = new Regex(patten); 
            Match m = r.Match(c.ToString()); 
            return m.Success;
        }

        /// <summary>
        /// 判断是否邮箱地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// 判断是否正整数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(this string data, out int result)
        {
            try
            {
                int tempResult;
                if (!int.TryParse(data, out tempResult))
                {
                    result = -1;
                    return false;
                }
                if (tempResult < 1)
                {
                    result = -1;
                    return false;
                }
                result = tempResult;
                return true;
            }
            catch
            {
                result = -1;
                return false;
            }
        }

        /// <summary>
        /// 判断是否正整数，并位于 min 和 max之间
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <param name="min"> </param>
        /// <param name="max"> </param>
        /// <returns></returns>
        public static bool IsInteger(this string data, out int result, int min = 0, int max = int.MaxValue)
        {
            try
            {
                int tempResult;
                if (!int.TryParse(data, out tempResult))
                {
                    result = -1;
                    return false;
                }
                if (tempResult < min || tempResult > max)
                {
                    result = -1;
                    return false;
                }
                result = tempResult;
                return true;
            }
            catch
            {
                result = -1;
                return false;
            }
        }

        /// <summary>
        /// 判断是否为null,empty,或由指定字符组成
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"> </param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrConsistBy(this string data, char element)
        {
            return string.IsNullOrEmpty(data) || data.All(c => c.Equals(element));
        }

        //判断是否由数字组成
        public static bool IsNumeric(this string str)
        {
            var n = str.Length;
            var mStr = "[0-9]{" + n.ToString() + ",}";
            return Regex.Match(str, mStr).Success;
        }

        //判断是否手机号码
        public static bool IsMobilePhone(this string str, string matchString = "")
        {
            string mStr = string.IsNullOrEmpty(matchString) ? @"^[1]+([35]|[86]|[38]|[37])+\d{9}" : matchString;
            return Regex.Match(str, mStr).Success;
        }
    }
}