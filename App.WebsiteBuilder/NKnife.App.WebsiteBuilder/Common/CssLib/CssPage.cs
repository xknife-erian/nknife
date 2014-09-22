using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jeelu
{
    public class CssPage
    {
        CssSectionCollection _sections = new CssSectionCollection();
        public CssSectionCollection Sections
        {
            get { return _sections; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (CssSection section in Sections)
            {
                sb.Append(section);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 解析CSS字符串的正则
        /// </summary>
        static Regex _regexParseCssPage = new Regex(@"[^{}]*\w*\{[^}]*\}",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

        public void LoadText(string cssText)
        {
            Debug.Assert(cssText != null);

            this.Sections.Clear();

            ///去掉输入字符串的首尾空格
            cssText = cssText.Trim();

            ///用正则解析字符串
            MatchCollection mc = _regexParseCssPage.Matches(cssText);

            int index = -1;
            foreach (Match m in mc)
            {
                ///两次Match中间的部分用CssOtherSection保存起来
                int lastNextIndex = index + 1;
                if (m.Index > lastNextIndex)
                {
                    CssOtherSection otherSection = new CssOtherSection(cssText.Substring(lastNextIndex, m.Index - lastNextIndex));
                    this.Sections.Add(otherSection);
                }
                index = m.Index + m.Length;

                ///保存匹配到的CssSection
                CssSection section = CssSection.Parse(m.Value);
                this.Sections.Add(section);
            }
        }

        static public CssPage Parse(string cssText)
        {
            CssPage page = new CssPage();
            page.LoadText(cssText);
            return page;
        }
    }
}
