using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jeelu
{
    public class CssSection
    {
        public string Name { get; private set; }

        CssPropertyCollection _properties = new CssPropertyCollection();
        public CssPropertyCollection Properties
        {
            get { return _properties; }
        }

        public CssSection(string name)
        {
            Name = name;
        }
        public CssSection()
        {
        }

        public override string ToString()
        {
            return ToString(true);
        }

        const string Indent = "   ";
        public string ToString(bool isSimple)
        {
            StringBuilder sb = new StringBuilder();

            ///输出name{
            bool hasName = !string.IsNullOrEmpty(Name);
            if (hasName)
            {
                sb.Append(Name).Append("{");
            }

            ///输出中间内容
            foreach (CssProperty property in Properties)
            {
                if (property.Value == null || property.Value.Trim() == "")
                {
                    continue;
                }
                
                ///加空格缩进来做格式处理
                if ((!isSimple) && hasName)
                {
                    sb.AppendLine(Indent);
                }

                ///实际内容
                sb.Append(property.ToString());

                ///加回车来做格式处理
                if (!isSimple)
                {
                    sb.AppendLine();
                }
            }

            ///输出}
            if (Name != null)
            {
                sb.Append("}");
            }

            return sb.ToString();
        }

        public void LoadText(string cssText)
        {
            this.Properties.Clear();
            cssText = cssText.Trim();

            string strName = null;
            ///有'{'字符表示是有name的
            if (cssText.IndexOf('{') > -1 && cssText.EndsWith("}"))
            {
                int leftBracketIndex = cssText.IndexOf('{');
                strName = cssText.Substring(0, leftBracketIndex);

                ///去掉name和{}
                cssText = cssText.Substring(leftBracketIndex + 1, cssText.Length - (leftBracketIndex + 1) - 1).Trim();
            }

            string[] arrStrPropertys = cssText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            this.Name = strName;

            foreach (string strProp in arrStrPropertys)
            {
                string strPropValue = strProp;
                ///处理注释
                MatchCollection mc = _regexCssComment.Matches(strPropValue);
                foreach (Match m in mc)
                {
                    string comment = m.Value;
                    this.Properties.Add(CssProperty.Parse(comment));
                }
                strPropValue = _regexCssComment.Replace(strPropValue, "");

                this.Properties.Add(CssProperty.Parse(strPropValue));
            }
        }

        static public CssSection Parse(string cssText)
        {
            CssSection cssSection = new CssSection();
            cssSection.LoadText(cssText);
            return cssSection;
        }

        static Regex _regexCssComment = new Regex(@"\<\!\-\-((?!\-\-\>).)*\-\-\>|\/\*((?!\*\/).)*\*\/", 
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
    }

    public class CssOtherSection : CssSection
    {
        public string Text { get; private set; }
        public CssOtherSection(string text)
            :base(Utility.Guid.NewGuid())
        {
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
