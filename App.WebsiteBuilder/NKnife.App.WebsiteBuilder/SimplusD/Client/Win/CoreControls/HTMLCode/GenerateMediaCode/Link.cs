using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class LINK
    {
        public string LinkHtml(
            string innerText,
    string linkUrl,
    string linkTarget,
    string linkTip,
    string linkAccesskey,
    string BookMark
    )
        {
            string linkCode = "";

            linkCode = "<a title=\""+linkTip+"\" accesskey=\"" + linkAccesskey + "\" tabindex=\""+BookMark+"\"  ";
            linkCode += "href=\"" + linkUrl + "\"  ";
            if (string.IsNullOrEmpty(innerText))
                innerText = linkTip;
            linkCode += "target=\"" + linkTarget + "\">" + innerText + "</a>";

            if (linkUrl == "")
                return "";
            else
                return linkCode;

        }
    }
}
