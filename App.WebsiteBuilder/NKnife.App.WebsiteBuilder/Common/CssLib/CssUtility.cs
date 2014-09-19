using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Jeelu
{
    public static class CssUtility
    {
        static private ActionReturn<string> _selectResources;
        static public void Initialize(ActionReturn<string> selectResources)
        {
            _selectResources = selectResources;
        }

        /// <summary>
        /// 选择资源文件中的图片
        /// </summary>
        /// <returns></returns>
        static public string SelectImageResource()
        {
            return _selectResources();
        }

        /// <summary>
        /// 不是此CSS属性的标准值。仍要使用吗？
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <returns>使用返回true，不使用返回false</returns>
        static public bool ShowNotStandard(string propertyValue)
        {
            string msg = string.Format("{0}不是此CSS属性的标准值。\r\n仍要使用吗？", propertyValue);
            return MessageBox.Show(msg, "SimplusD！", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }
        /// <summary>
        /// 请用数字代替
        /// </summary>
        /// <param name="propertyValue"></param>
        static public void ShowReplaceMsg(string propertyValue)
        {
            string msg = string.Format("请用数字代替{0}。", propertyValue);
            MessageBox.Show(msg, "SimplusD！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 不是此CSS属性的标准值
        /// </summary>
        /// <param name="propertyValue"></param>
        static public void ShowNotStandardSingleBtn(string propertyValue)
        {
            string msg = string.Format("{0}包含非法CSS属性。", propertyValue);
            MessageBox.Show(msg, "SimplusD！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 不是一个有效的颜色值。输入十六进制值(#RRGGBB)或一个标准的颜色名称
        /// </summary>
        /// <param name="propertyValue"></param>
        static public void ShowNotStandardColorMsg(string propertyValue)
        {
            string msg = string.Format("{0}不是一个有效的颜色值。输入十六进制值(#RRGGBB)\r\n或一个标准的颜色名称。",propertyValue);
            MessageBox.Show(msg, "SimplusD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static private Regex _regexColor = new Regex(@"^(#?[\da-f]+)$",
            RegexOptions.Compiled|RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);
        /// <summary>
        /// 判断字符串是否有效的颜色值。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public bool IsEffectiveColor(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return _regexColor.IsMatch(input);
        }


        static private Regex _regexBackgroundPosition = new Regex(@"(?<=\s|^)(top|left|center|bottom|right|[.0-9]*(px|pt|in|cm|mm|pc|em|ex|%))(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBackgroundPositionH = new Regex(@"(?<=\s|^)(left|right)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBackgroundPositionV = new Regex(@"(?<=\s|^)(top|bottom)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBackgroundPositionNoV = new Regex(@"(?<=\s|^)(left|center|right|[.0-9]*(px|pt|in|cm|mm|pc|em|ex|%))(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBackgroundPositionNoH = new Regex(@"(?<=\s|^)(top|bottom|center|[.0-9]*(px|pt|in|cm|mm|pc|em|ex|%))(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// 通过background-position的css值获取其水平和垂直的值。Key是水平值；Value是垂直值。(Key和Value都有为""的可能)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public KeyValuePair<string, string> ParseBackgroundPosition(string value)
        {
            string strH = "";   //水平值
            string strV = "";   //垂直值

            ///先找有没有特定的垂直值
            Match mV = _regexBackgroundPositionV.Match(value);
            if (mV.Value != "")
            {
                strV = mV.Value;
                Match m = _regexBackgroundPositionNoV.Match(value);
                strH = m.Value;
            }
            else
            {
                ///没有特定的垂直值，那么找特定的水平值
                Match mH = _regexBackgroundPositionH.Match(value);
                if (mH.Value != "")
                {
                    strH = mH.Value;
                    Match m = _regexBackgroundPositionNoH.Match(value);
                    strV = m.Value;
                }
                else
                {
                    ///既没有特定水平值，也没有特定垂直值，则按顺序找，第一个是水平，第二个是垂直
                    Match mFirst = _regexBackgroundPosition.Match(value);
                    if (mFirst.Value != "")
                    {
                        strH = mFirst.Value;
                        if (mFirst.Index + mFirst.Length < value.Length)
                        {
                            Match mSecond = _regexBackgroundPosition.Match(value, mFirst.Index + mFirst.Length + 1);
                            strV = mSecond.Value;
                        }
                    }
                }
            }

            return new KeyValuePair<string, string>(strH, strV);
        }

        static Regex _regexFieldUnit = new Regex(@"(?<field>(\d*\.)?\d*)(?<unit>px|px|pt|in|cm|mm|pc|em|ex|%)?",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        /// <summary>
        /// 解析值和单位。返回为false则解析失败，不是数字+单位的形式。
        /// fieldUnit即为解析出来的值和单位，Key是值；Value是单位。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldUnit">fieldUnit即为解析出来的值和单位，Key是值；Value是单位。</param>
        /// <returns></returns>
        static public bool ParseFieldUnit(string value, out KeyValuePair<string, string> fieldUnit)
        {
            Debug.Assert(value != null);
            fieldUnit = new KeyValuePair<string, string>();

            Match m = _regexFieldUnit.Match(value);
            if (m.Value == "")
            {
                return false;
            }

            fieldUnit = new KeyValuePair<string, string>(m.Groups["field"].Value, m.Groups["unit"].Value);
            return true;
        }
    }
}
