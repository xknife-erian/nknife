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
    }
}