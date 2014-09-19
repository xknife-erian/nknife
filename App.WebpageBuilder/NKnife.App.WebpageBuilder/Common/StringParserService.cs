using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Jeelu
{
    public static class StringParserService
    {
        static readonly Regex parseRegex = new Regex(@"\$\{(?<sub>Date|Time|res:(?<content>[._a-zA-Z0-9]+)|pro:(?<content>[._a-zA-Z0-9]+))\}", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        static private ActionReturn<string,string> _actionGetResourceText;
        static public void Initialize(ActionReturn<string, string> getResourceText)
        {
            _actionGetResourceText = getResourceText;
        }

        /// <summary>
        /// 解析字符串。
        /// ${res:MainMenu.FileMenu.Name}表示资源文件(Debug\CHS\ResourceText.Xml)中的字符串。
        /// ${pro:Main}表示全局属性的值。
        /// ${Date}表示当前日期。
        /// ${Time}表示当前时间。
        /// 其它的原封不动输出。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Parse(string str)
        {
            return parseRegex.Replace(str, new MatchEvaluator(CallbackReplace));
        }
        private static string CallbackReplace(Match m)
        {
            //检查其类型
            string str = m.Groups["sub"].Value.Substring(0,4).ToLower();
            switch (str)
            {
                case "res:":
                    {
                        //return Service.Resource.GetResourceText(m.Groups["content"].Value);
                        return _actionGetResourceText(m.Groups["content"].Value);
                    }
                default:
                    Debug.Fail("未处理的StringParse类型:" + str);
                    return m.Value;
            }
        }
    }
}
