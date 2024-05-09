using System.Text.RegularExpressions;
using NKnife.ShareResources;

namespace NKnife.Util
{
    /// <summary>
    /// 一些正则相关的帮助应用
    /// </summary>
    public static class RegexUtil
    {
        /// <summary>
        /// 正则：验证邮件地址。
        /// 正则表达式来自：http://RegexLib.com
        /// </summary>
        public static Regex EmailAddress => new(RegexString.RegexStr_SimpleEmail, RegexOptions.Multiline | RegexOptions.ExplicitCapture);

        /// <summary>
        /// 正则：验证Url地址。
        /// </summary>
        public static Regex HttpUrl => new(RegexString.RegexStr_HttpUrl);

        /// <summary>
        /// 正则：回车符“\r\n”。
        /// </summary>
        public static Regex Br => new(RegexString.RegexStr_Br, RegexOptions.IgnoreCase);

        /// <summary>
        /// 正则：yy-mm-dd字符串。
        /// </summary>
        public static Regex Date => new(RegexString.RegexStr_Date);

        /// <summary>
        /// 正则：00:00:00 字符串。
        /// </summary>
        public static Regex Time => new(RegexString.RegexStr_Time);

        /// <summary>
        /// 正则：规范的文件名
        /// </summary>
        public static Regex FileName => new(RegexString.RegexStr_FileName);
    }
}
