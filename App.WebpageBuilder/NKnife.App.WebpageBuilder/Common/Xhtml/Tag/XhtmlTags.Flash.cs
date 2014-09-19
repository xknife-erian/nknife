using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu
{
    public partial class XhtmlTags
    {
        //<OBJECT title="" style="WIDTH: 350px; HEIGHT: 262px" accessKey="" 

        //codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,

        //0 align=Default classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000><PARAM NAME="_cx" 

        //VALUE="9260">
        //<PARAM NAME="_cy" VALUE="6932">
        //<PARAM NAME="FlashVars" VALUE="">
        //<PARAM NAME="Movie" VALUE="${srs_3cb9e330dac5408095fdcccd348fac3c}">
        //<PARAM NAME="Src" VALUE="${srs_3cb9e330dac5408095fdcccd348fac3c}">
        //<PARAM NAME="WMode" VALUE="Window">
        //<PARAM NAME="Play" VALUE="-1">
        //<PARAM NAME="Loop" VALUE="-1">
        //<PARAM NAME="Quality" VALUE="High">
        //<PARAM NAME="SAlign" VALUE="">
        //<PARAM NAME="Menu" VALUE="-1">
        //<PARAM NAME="Base" VALUE="">
        //<PARAM NAME="AllowScriptAccess" VALUE="">
        //<PARAM NAME="Scale" VALUE="ShowAll">
        //<PARAM NAME="DeviceFont" VALUE="0">
        //<PARAM NAME="EmbedMovie" VALUE="0">
        //<PARAM NAME="BGColor" VALUE="">
        //<PARAM NAME="SWRemote" VALUE="">
        //<PARAM NAME="MovieData" VALUE="">
        //<PARAM NAME="SeamlessTabbing" VALUE="1">
        //<PARAM NAME="Profile" VALUE="0">
        //<PARAM NAME="ProfileAddress" VALUE="">
        //<PARAM NAME="ProfilePort" VALUE="0">
        //<PARAM NAME="AllowNetworking" VALUE="all">
        //<PARAM NAME="AllowFullScreen" VALUE="false">
        //<embed width="350px" height="262px" src="${srs_3cb9e330dac5408095fdcccd348fac3c}" hspace="0" 

        //vspace="0"align="Default" quality=Hight 

        //pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-

        //flash" scale=""></embed>
        //</OBJECT>
        public class Flash : XhtmlTags.Object
        {
            internal Flash() { }

            //<IMG style="WIDTH: 500px; HEIGHT: 667px" alt="" hspace=11 
            //src="${srs_d43bab22b0a2469287257db914d95dd4}" align=baseline vspace=22 border=33 name=pic>

            public void Builder(CssSection style, string hspace, string src, Xhtml.Align align, string quality, int loop, int autostart, string vspace, string border, string title)
            {
                string width = style.Properties["width"];
                string height = style.Properties["height"];
                string flashHtml = "";
                flashHtml += "<OBJECT title=\"" + title + "\" style=\"" + style + "\" accessKey=\"\" ";
                flashHtml += "codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" align=\"" + align + "\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\">";
                //flashHtml += "<PARAM NAME=\"_cx\" VALUE=\"9260\">";
                //flashHtml += "<PARAM NAME=\"_cy\" VALUE=\"6932\">";
                //flashHtml += "<PARAM NAME=\"FlashVars\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"Movie\" VALUE=\"$" + src + "\">";
                //flashHtml += "<PARAM NAME=\"Src\" VALUE=\"$" + src + "\">";
                //flashHtml += "<PARAM NAME=\"WMode\" VALUE=\"Window\">";
                //flashHtml += "<PARAM NAME=\"Play\" VALUE=\"" + autostart + "\">";
                //flashHtml += "<PARAM NAME=\"Loop\" VALUE=\"" + loop + "\">";
                //flashHtml += "<PARAM NAME=\"Quality\" VALUE=\"" + quality + "\">";
                //flashHtml += "<PARAM NAME=\"SAlign\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"Menu\" VALUE=\"-1\">";
                //flashHtml += "<PARAM NAME=\"Base\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"AllowScriptAccess\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"Scale\" VALUE=\"ShowAll\">";
                //flashHtml += "<PARAM NAME=\"DeviceFont\" VALUE=\"0\">";
                //flashHtml += "<PARAM NAME=\"EmbedMovie\" VALUE=\"0\">";
                //flashHtml += "<PARAM NAME=\"BGColor\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"SWRemote\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"MovieData\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"SeamlessTabbing\" VALUE=\"1\">";
                //flashHtml += "<PARAM NAME=\"Profile\" VALUE=\"0\">";
                //flashHtml += "<PARAM NAME=\"ProfileAddress\" VALUE=\"\">";
                //flashHtml += "<PARAM NAME=\"ProfilePort\" VALUE=\"0\">";
                //flashHtml += "<PARAM NAME=\"AllowNetworking\" VALUE=\"all\">";
                //flashHtml += "<PARAM NAME=\"AllowFullScreen\" VALUE=\"false\">";
                flashHtml += "<embed width=\"" + width + "\" height=\"" + height + "\" src=\"" + src + "\" ";
                flashHtml += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" align=\"" + align + "\" quality=\"" + quality + "\" ";
                flashHtml += "pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"";
                flashHtml += " scale=\"\"></embed>";
                flashHtml += "</OBJECT>";
                //this.te
                this.InnerXml = flashHtml;
            }

            public XmlElement ObjectEle
            {
                get
                {
                    return (XmlElement)this.AsXmlNode.ChildNodes.Item(0);//.GetElementsByName("object")[0];
                }
            }
            public XmlElement EmbedEle
            {
                get
                {
                    return (XmlElement)ObjectEle.ChildNodes.Item(0);//.GetElementsByName("embed")[0];
                }
            }
            public void SetStyle(string width, string height)
            {
                string styleStr = this.GetAttribute("style");
                CssSection section = CssSection.Parse(styleStr);
                section.Properties["width"] = width;
                section.Properties["height"] = height;
                this.SetAttribute("style", section.ToString());

                EmbedEle.SetAttribute("width", width);
                EmbedEle.SetAttribute("height", height);
            }

            public string Hspace
            {
                get { return this.Attributes["hspace"].Value; }
                set 
                { 
                    this.SetAttribute("hspace", value);
                    EmbedEle.SetAttribute("hspace", value); 
                }
            }
            /*
            public string Src
            {
                get { return src; }
                set { src = value; }
            }


            public Xhtml.Align Align
            {
                get { return align; }
                set { align = value; }
            }

            public string Quality
            {
                get { return quality; }
                set { quality = value; }
            }

            public int Loop
            {
                get { return loop; }
                set { loop = value; }
            }

            public int Autostart
            {
                get { return autostart; }
                set { autostart = value; }
            }

            public string Vspace
            {
                get { return vspace; }
                set { vspace = value; }
            }

            public string Border
            {
                get { return border; }
                set { border = value; }
            }

            public string Title
            {
                get { return title; }
                set { title = value; }
            }*/
        }
    }
}
