using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    static public class ValidateService
    {
        /// <summary>
        /// 包括验证规则的容器
        /// </summary>
        static Dictionary<string, ValidateRule> _vlidateRules = new Dictionary<string, ValidateRule>();
        
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ValidateService()
        {
            string validateFile = PathService.CL_Validate;
            XmlDocument doc = new XmlDocument();
            doc.Load(validateFile);
            
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (node.Name)
                {
                    case "notnull":
                        {
                            ValidateRule rule = new NotNullValidateRule("NotNull", node.Attributes["errortext"].Value);
                            _vlidateRules.Add(rule.Name, rule);
                            break;
                        }
                    case "regex":
                        {
                            ValidateRule rule = new RegexValidateRule(node.Attributes["name"].Value,
                                node.Attributes["errortext"].Value, node.Attributes["text"].Value);
                            _vlidateRules.Add(rule.Name, rule);
                            break;
                        }
                    default:
                        throw new Exception("未知的验证类型:" + node.Name);
                }
            }
        }

        /// <summary>
        /// 验证函数
        /// </summary>
        /// <param name="validateText"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        static public bool Validate(string validateText,Control sender,out string errorText)
        {
            errorText = null;
            string[] validateTextArr = validateText.Split(new char[] { '&' },StringSplitOptions.RemoveEmptyEntries);
            foreach (string vali in validateTextArr)
            {
                ValidateRule rule = _vlidateRules[vali];
                if (!rule.Validate(sender.Text))
                {
                    errorText = rule.ErrorText;
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 验证函数
        /// </summary>
        /// <param name="validateText"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        static public bool Validate(string validateText, Control sender)
        {
            string errorText = null;
            return Validate(validateText, sender, out errorText);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    abstract class ValidateRule
    {
        public string Name;
        public string ErrorText;
        public ValidateRule(string name, string errorText)
        {
            this.Name = name;
            this.ErrorText = errorText;
        }

        public abstract bool Validate(string input);
    }

    /// <summary>
    /// 
    /// </summary>
    class NotNullValidateRule : ValidateRule
    {
        public NotNullValidateRule(string name, string errorText)
            : base(name, errorText)
        {
        }

        public override bool Validate(string input)
        {
            return input != "";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class RegexValidateRule : ValidateRule
    {
        public Regex Reg;
        public RegexValidateRule(string name, string errorText, string strRegex)
            : base(name, errorText)
        {
            this.Reg = new Regex(strRegex,RegexOptions.Singleline);
        }

        public override bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            return Reg.IsMatch(input);
        }
    }
}
