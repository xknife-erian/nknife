using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class Email
    {
        /// <summary>
        /// 生成Email的代码
        /// </summary>
        /// <param name="showText">显示给用户看到的文字</param>
        /// <param name="email">实际链接的地地址</param>
        /// <returns></returns>
        public string EmailHtml(string showText,string email)
        {
            string emailCode = "";
            if (showText == "" && email == "")
                return "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xmlEle = xmlDoc.CreateElement("a");

            if (showText != "")
                xmlEle.InnerText = showText;
            else 
                xmlEle.InnerText = email;
            if (email != "")
                xmlEle.SetAttribute("href", "mailto:"+email);
            else
                xmlEle.SetAttribute("href", "#");
            XmlNode node = xmlDoc.AppendChild(xmlEle);
            emailCode = node.OuterXml;
            return emailCode;
        }
    }
}
