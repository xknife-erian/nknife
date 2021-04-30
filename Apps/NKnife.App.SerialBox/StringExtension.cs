using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class StringExtension
    {
        /// <summary>
        ///     判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseSensitive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayIndex(this string srcString, string[] stringArray, bool caseSensitive)
        {
            for (var i = 0; i < stringArray.Length; i++)
            {
                if (caseSensitive)
                {
                    if (srcString.ToLower() == stringArray[i].ToLower())
                        return i;
                }
                else
                {
                    if (srcString == stringArray[i])
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        ///     判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayIndex(this string srcString, string[] stringArray)
        {
            return GetInArrayIndex(srcString, stringArray, true);
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseSensitive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string srcString, string[] stringArray, bool caseSensitive)
        {
            return GetInArrayIndex(srcString, stringArray, caseSensitive) >= 0;
        }

        /// <summary>
        ///     判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="srcString">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string srcString, string[] stringArray)
        {
            return InArray(srcString, stringArray, false);
        }

        /// <summary>
        ///     删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimTail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            for (var i = str.Length; i >= 0; i--)
                if (str[i].Equals(' ') || str[i].Equals('\r') || str[i].Equals('\n'))
                    str = str.Remove(i, 1);
            return str;
        }

        /// <summary>
        ///     去除字符串尾部的“0”字符
        /// </summary>
        public static string TrimZero(this string str)
        {
            var n = str.LastIndexOf('.');
            if (n > 0)
            {
                var allIsZero = true;
                int i;
                for (i = str.Length - 1; i >= n + 1; i--)
                    if (str[i] != '0')
                    {
                        allIsZero = false;
                        break;
                    }
                var result = !allIsZero ? str.Substring(0, i + 1) : str.Substring(0, n);
                return result;
            }
            if (str.IsEmptyAndZero())
                return "0";
            return str;
        }

        /// <summary>
        ///     是否是Null,空,全部是空白或全部为0的字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>
        ///     <c>true</c> if [is composed by zero] [the specified STR]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmptyAndZero(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return true;
            var n = str.Length;
            var mStr = $"[0]{{{n},}}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>
        ///     是否是拉丁字母（大小写均可）
        /// </summary>
        public static bool IslLatinLetter(this char c)
        {
            const string patten = "^[A-Za-z]+$";
            var r = new Regex(patten);
            var m = r.Match(c.ToString());
            return m.Success;
        }

        /// <summary>
        ///     判断是否邮箱地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        ///     判断是否正整数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(this string data, out int result)
        {
            try
            {
                if (!int.TryParse(data, out var tempResult))
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
        ///     判断是否整数，并位于 min 和 max之间
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
                if (!int.TryParse(data, out var tempResult))
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
        ///     判断是否为null,empty,或由指定字符组成
        /// </summary>
        /// <param name="data"></param>
        /// <param name="element"> </param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrConsistBy(this string data, char element)
        {
            return string.IsNullOrEmpty(data) || data.All(c => c.Equals(element));
        }

        /// <summary>
        ///     判断是否由数字组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            var n = str.Length;
            var mStr = "[0-9]{" + n + ",}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>
        ///     判断字符串是否能被(filters)过滤
        ///     strictMatch=true时，是严格过滤模式，src必须完全等于filters中的某一项，才算Match，return true
        ///     strictMatch=false时，是宽松过滤模式，src只要包含filters中的某一项，算Match，return true
        /// </summary>
        /// <param name="src"></param>
        /// <param name="filters"></param>
        /// <param name="strictMatch"></param>
        /// <returns></returns>
        public static bool MatchFilters(this string src, string[] filters, bool strictMatch = false)
        {
            if (filters == null)
                return false;
            foreach (var filter in filters)
                if (strictMatch)
                {
                    if (src.Equals(filter))
                        return true;
                }
                else
                {
                    if (src.IndexOf(filter, StringComparison.Ordinal) > -1)
                        return true;
                }
            return false;
        }

        /// <summary>
        ///     将用分隔符分隔ASCII字节的字符串转换成字节数组（去除分隔符）
        /// </summary>
        /// <param name="arrayString">用分隔符分隔ASCII字节的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>去除分隔符的字符串的字节数组</returns>
        public static byte[] ToBytes(this string arrayString, params char[] separator)
        {
            var newStr = arrayString;
            foreach (var sep in separator)
                newStr = newStr.Replace(sep.ToString(), "");
            return Encoding.ASCII.GetBytes(newStr);
        }

        #region 与中文有关的判断

        /// <summary>
        ///     给定一个字符串，判断其是否是中文字符串
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns>
        ///     <c>true</c> if [is chinese letter] [the specified SRC]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsChineseLetter(this string src)
        {
            for (int k = 0; k < src.Length; k++)
            {
                // \u4e00-\u9fa5 汉字的范围。
                // ^[\u4e00-\u9fa5]$ 汉字的范围的正则
                var rx = new Regex("^[\u4e00-\u9fa5]$");
                var match = rx.IsMatch(src.Substring(k, 1));
                if (!match)
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     给定一个字符串，判断其是否仅仅包含有汉字
        /// </summary>
        public static bool IsOnlyContainsChinese(this string srcStr)
        {
            var words = srcStr.ToCharArray();
            foreach (var word in words)
                if (IsGbCode(word.ToString()) || IsGbkCode(word.ToString())) // it is a GB2312 or GBK chinese word
                    continue;
                else
                    return false;
            return true;
        }

        /// <summary>
        ///     判断一个word是否为GB2312编码的汉字
        /// </summary>
        public static bool IsGbCode(this string word)
        {
            var bytes = Encoding.GetEncoding("GB2312").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code or other code
                return false;
            var byte1 = bytes[0];
            var byte2 = bytes[1];
            if (byte1 >= 176 && byte1 <= 247 && byte2 >= 160 && byte2 <= 254) //判断是否是GB2312
                return true;
            return false;
        }

        /// <summary>
        ///     判断一个word是否为GBK编码的汉字
        /// </summary>
        /// <param></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsGbkCode(this string word)
        {
            var bytes = Encoding.GetEncoding("GBK").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
                return false;
            var byte1 = bytes[0];
            var byte2 = bytes[1];
            if (byte1 >= 129 && byte1 <= 254 && byte2 >= 64 && byte2 <= 254) //判断是否是GBK编码
                return true;
            return false;
        }

        /// <summary>
        ///     判断一个word是否为Big5编码的汉字
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsBig5Code(this string word)
        {
            var bytes = Encoding.GetEncoding("Big5").GetBytes(word);
            if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
                return false;
            var byte1 = bytes[0];
            var byte2 = bytes[1];
            if (byte1 >= 129 && byte1 <= 254 && (byte2 >= 64 && byte2 <= 126 || byte2 >= 161 && byte2 <= 254)) //判断是否是Big5编码
                return true;
            return false;
        }

        #endregion

    }
}