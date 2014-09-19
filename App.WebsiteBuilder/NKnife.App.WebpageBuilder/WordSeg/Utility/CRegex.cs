using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// ������ʽ��
    /// ��װһЩ���õ�������ʽ���ͺ���
    /// </summary>
    public class CRegex
    {
        /// <summary>
        /// ��ȡ��������ʽƥ��������ַ������б�
        /// </summary>
        /// <param name="text">Ҫת����text�ı�</param>
        /// <param name="regx">���ڱ��ʽ</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <param name="output">����ַ����б�,�����Ѿ�ʵ����</param>
        /// <returns>���û���κ�ƥ��,����false</returns>
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
        /// ��ȡ��������ʽƥ��������ַ������б�
        /// </summary>
        /// <param name="text">Ҫת����text�ı�</param>
        /// <param name="regx">���ڱ��ʽ</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <param name="output">����ַ����б�,�����Ѿ�ʵ����</param>
        /// <returns>���û���κ�ƥ��,����false</returns>
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
        /// �÷ָʽ������ƥ���ַ����б�,��ȥ����һ��ƥ��ֵ
        /// </summary>
        /// <param name="text">Ҫת����text�ı�</param>
        /// <param name="regx">���ڱ��ʽ</param>
        /// <param name="output">����ַ����б�,�����Ѿ�ʵ����</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <returns>���û���κ�ƥ��,����false</returns>
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
        /// ��ȡ����������ʽ���ַ���֮��
        /// </summary>
        /// <param name="text">Ҫת����text�ı�</param>
        /// <param name="regx">���ڱ��ʽ</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <returns>����������ʽ��ת���ַ���֮��</returns>
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
        /// һ���滻����ַ���
        /// </summary>
        /// <param name="Input">Ҫ�滻���ַ���</param>
        /// <param name="Expressions">
        /// ������ʽ�������¸�ʽ
        /// 
        /// </param>
        /// <param name="ReplaceStr">
        /// �����¹����滻
        /// ��ʽΪ"${Name1}Replace1${Name2}Replace2"
        /// </param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <returns>�滻����ַ���</returns>
        /// <example>
        ///             string Input = "Test1,Test2;"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///    string RegularExpressions = "((Test1)|(Test2))"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    string ReplaceStr = "${Test1}abc${Test2}def"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    string expected = "abc,def;";
        ///    string actual;
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup δ���������ֵ��");
        ///
        ///    Input = ";Test1,Test2"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    RegularExpressions = "((Test1)|(Test2))"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    ReplaceStr = "${Test1}abc${Test2}def"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    expected = ";abc,def";
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup δ���������ֵ��");
        ///
        ///    Input = ";Test1,Test2"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    RegularExpressions = "(Test1),(Test2)"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    ReplaceStr = "${Test1}abc${Test2}def"; // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///
        ///    expected = ";abc,def";
        ///
        ///    actual = General.CRegex.ReplaceMutiGroup(Input, RegularExpressions, ReplaceStr);
        ///
        ///    Assert.AreEqual(expected, actual, "General.CRegex.ReplaceMutiGroup δ���������ֵ��");
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
        /// �ָ��ַ���
        /// </summary>
        /// <param name="Src">Ҫ�ָ��Դ�ַ���</param>
        /// <param name="SplitStr">�ָ��,������������ʽ</param>
        /// <returns>�ָ����ַ�������</returns>
        public static String[] Split(String Src, String SplitStr)
        {
            Regex reg = new Regex(SplitStr);
            return reg.Split(Src);
        }


        /// <summary>
        /// �ָ��ַ���
        /// </summary>
        /// <param name="Src">Ҫ�ָ��Դ�ַ���</param>
        /// <param name="SplitStr">�ָ��,������������ʽ</param>
        /// <param name="option">�ָ�ѡ��</param>
        /// <returns>�ָ����ַ�������</returns>
        public static String[] Split(String Src, String SplitStr, RegexOptions option)
        {
            Regex reg = new Regex(SplitStr, option);
            return reg.Split(Src);
        }

        /// <summary>
        /// �滻�ַ���
        /// </summary>
        /// <param name="text">Ҫ�滻���ַ���</param>
        /// <param name="regx">Ҫ�滻��������ʽ</param>
        /// <param name="newText">�滻�ɵ��ַ���</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
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
