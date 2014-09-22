using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 正则表达式类
    /// 封装一些常用的正则表达式解释函数
    /// </summary>
    public class CRegex
    {
        /// <summary>
        /// 获取和正则表达式匹配的所有字符串的列表
        /// </summary>
        /// <param name="text">要转换的text文本</param>
        /// <param name="regx">正在表达式</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="output">输出字符串列表,必须已经实例化</param>
        /// <returns>如果没有任何匹配,返回false</returns>
        static public bool GetMatchStrings(String text, String regx, 
            bool ignoreCase, ref List<string> output)
        {
            if (output == null)
            {
                Debug.Assert(false);
                return false;
            }

            output.Clear();

            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            MatchCollection m = reg.Matches(text);

            if (m.Count == 0)
                return false;

            for (int j = 0; j < m.Count; j++)
            {
                int count = m[j].Groups.Count;

                for (int i = 1; i < count; i++)
                {
                    output.Add(m[j].Groups[i].Value.Trim());
                }
            }

            return true;

        }


        /// <summary>
        /// 获取和正则表达式匹配的所有字符串的列表
        /// </summary>
        /// <param name="text">要转换的text文本</param>
        /// <param name="regx">正在表达式</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="output">输出字符串列表,必须已经实例化</param>
        /// <returns>如果没有任何匹配,返回false</returns>
        static public bool GetSingleMatchStrings(String text, String regx,
            bool ignoreCase, ref List<string> output)
        {
            if (output == null)
            {
                Debug.Assert(false);
                return false;
            }

            output.Clear();

            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            MatchCollection m = reg.Matches(text);

            if (m.Count == 0)
                return false;

            for (int j = 0; j < m.Count; j++)
            {
                int count = m[j].Groups.Count;

                if (count > 0)
                {
                    output.Add(m[j].Groups[count-1].Value.Trim());
                }
            }

            return true;

        }


        /// <summary>
        /// 得分割方式的正则匹配字符串列表,并去除第一个匹配值
        /// </summary>
        /// <param name="text">要转换的text文本</param>
        /// <param name="regx">正在表达式</param>
        /// <param name="output">输出字符串列表,必须已经实例化</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>如果没有任何匹配,返回false</returns>
        static public bool GetSplitWithoutFirstStrings(String text, String regx, 
            bool ignoreCase, ref List<string> output)
        {

            if (output == null)
            {
                Debug.Assert(false);
                return false;
            }

            output.Clear();

            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            String[] strs = reg.Split(text);
            if (strs == null)
            {
                return false;
            }

            if (strs.Length <= 1)
            {
                return false;
            }

            for (int j = 1; j < strs.Length; j++)
            {
                output.Add(strs[j]);
            }

            return true;

        }


        static public String GetMatch(String text, String regx, bool ignoreCase)
        {
            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            String ret = "";
            Match m = reg.Match(text);

            if (m.Groups.Count > 0)
            {
                ret = m.Groups[m.Groups.Count-1].Value;
            }

            return ret;
        }

        /// <summary>
        /// 获取符合正则表达式的字符串之和
        /// </summary>
        /// <param name="text">要转换的text文本</param>
        /// <param name="regx">正在表达式</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>符合正则表达式的转换字符串之和</returns>
        static public String GetMatchSum(String text, String regx, bool ignoreCase)
        {
            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            String ret = "";
            Match m = reg.Match(text);

            for (int i = 1; i < m.Groups.Count; i++)
            {
                ret += m.Groups[i].Value;
            }
            return ret;
        }


        /// <summary>
        /// 一次替换多个字符串
        /// </summary>
        /// <param name="Input">要替换的字符串</param>
        /// <param name="Expressions">
        /// 正则表达式，按如下格式
        /// 
        /// </param>
        /// <param name="ReplaceStr">
        /// 按如下规则替换
        /// 格式为"${Name1}Replace1${Name2}Replace2"
        /// </param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>替换后的字符串</returns>
        /// <example>
        ///             string Input = "Test1,Test2;"; // TODO: 初始化为适当的值
        ///    string RegularExpressions = "((Test1)|(Test2))"; // TODO: 初始化为适当的值
        ///
        ///    string ReplaceStr = "${Test1}abc${Test2}def"; // TODO: 初始化为适当的值
        ///
        ///    string expected = "abc,def;";
        ///    string actual;
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup 未返回所需的值。");
        ///
        ///    Input = ";Test1,Test2"; // TODO: 初始化为适当的值
        ///
        ///    RegularExpressions = "((Test1)|(Test2))"; // TODO: 初始化为适当的值
        ///
        ///    ReplaceStr = "${Test1}abc${Test2}def"; // TODO: 初始化为适当的值
        ///
        ///    expected = ";abc,def";
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup 未返回所需的值。");
        ///
        ///    Input = ";Test1,Test2"; // TODO: 初始化为适当的值
        ///
        ///    RegularExpressions = "(Test1),(Test2)"; // TODO: 初始化为适当的值
        ///
        ///    ReplaceStr = "${Test1}abc${Test2}def"; // TODO: 初始化为适当的值
        ///
        ///    expected = ";abc,def";
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup 未返回所需的值。");
        ///
        ///
        /// 
        /// </example>
        public static String ReplaceMutiGroup(String Input, String Expressions, String ReplaceStr, bool ignoreCase)
        {
            Regex regx = new Regex(@"\$\{(?<Name>[^\}]+)\}(?<Value>[^$]*)");

            MatchCollection mn = regx.Matches(ReplaceStr);
            Hashtable ht = new Hashtable(128);

            foreach (Match m in mn)
            {
                if (ignoreCase)
                {
                    ht.Add(m.Groups["Name"].Value.ToLower(), m.Groups["Value"].Value);
                }
                else
                {
                    ht.Add(m.Groups["Name"].Value, m.Groups["Value"].Value);
                }
            }

            if (ignoreCase)
            {
                regx = new Regex(Expressions, RegexOptions.IgnoreCase);
            }
            else
            {
                regx = new Regex(Expressions);
            }

            mn = regx.Matches(Input);
            int startPos = 0 ;


            StringBuilder ret = new StringBuilder(4096);

            foreach (Match m in mn)
            {
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    if (m.Groups[i].Index < startPos)
                        continue;

                    String hashCode;
                    if (ignoreCase)
                    {
                        hashCode = m.Groups[i].Value.ToLower();
                    }
                    else
                    {
                        hashCode = m.Groups[i].Value;
                    }

                    object obj = ht[hashCode];

                    if (obj != null)
                    {
                        String RepValue = obj.ToString();
                        ret.Append(Input.Substring(startPos, m.Groups[i].Index - startPos));
                        ret.Append(RepValue);
                        startPos = m.Groups[i].Index + m.Groups[i].Length;
                    }
                }
            }

            if (startPos < Input.Length)
            {
                ret.Append(Input.Substring(startPos, Input.Length - startPos));
            }

            return ret.ToString();
        }
    
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="Src">要分割的源字符串</param>
        /// <param name="SplitStr">分割符,可以是正则表达式</param>
        /// <returns>分割后的字符串集合</returns>
        public static String[] Split(String Src, String SplitStr)
        {
            Regex reg = new Regex(SplitStr);
            return reg.Split(Src);
        }


        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="Src">要分割的源字符串</param>
        /// <param name="SplitStr">分割符,可以是正则表达式</param>
        /// <param name="option">分割选项</param>
        /// <returns>分割后的字符串集合</returns>
        public static String[] Split(String Src, String SplitStr, RegexOptions option)
        {
            Regex reg = new Regex(SplitStr, option);
            return reg.Split(Src);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="text">要替换的字符串</param>
        /// <param name="regx">要替换的正则表达式</param>
        /// <param name="newText">替换成的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static String Replace(String text, String regx, String newText, bool ignoreCase)
        {
            Regex reg;

            if (ignoreCase)
            {
                reg = new Regex(regx, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            else
            {
                reg = new Regex(regx, RegexOptions.Singleline);
            }

            return reg.Replace(text, newText);

        }

    }
}
