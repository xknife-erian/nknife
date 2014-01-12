using System.Linq;
using System.Text.RegularExpressions;

namespace NKnife.Extensions
{
    public static class StringExtension
    {
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
        public static bool IsInteger(this string data, out int result,int min = 0,int max = int.MaxValue)
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
        public static bool IsNullOrEmptyOrConsistBy(this string data,char element)
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
        public static bool IsMobilePhone(this string str,string matchString = "")
        {
            string mStr = string.IsNullOrEmpty(matchString) ? @"^[1]+([35]|[86]|[38]|[37])+\d{9}" : matchString;
            return Regex.Match(str, mStr).Success;
        }
    }
}
