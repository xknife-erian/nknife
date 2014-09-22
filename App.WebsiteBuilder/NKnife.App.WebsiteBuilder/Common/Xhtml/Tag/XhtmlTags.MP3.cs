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
        public class MP3 : XhtmlTags.Object
        {
            internal MP3() { }

            //<IMG style="WIDTH: 500px; HEIGHT: 667px" alt="" hspace=11 
            //src="${srs_d43bab22b0a2469287257db914d95dd4}" align=baseline vspace=22 border=33 name=pic>

            public void Builder(CssSection style, string hspace, string src, Xhtml.Align align, string quality, int loop, int autostart, string vspace, string border, string title, string scale)
            {
                string width = style.Properties["width"];
                string height = style.Properties["height"];
                string mp3Code = "";
                mp3Code += "<object style=\"WIDTH: " + width + "; HEIGHT: " + height + " \"";
                mp3Code += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                mp3Code += "classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">";
                mp3Code += "<param name=\"_ExtentX\" value=\"8599\" /> ";
                mp3Code += "<param name=\"_ExtentY\" value=\"3916\" />";
                mp3Code += "<param name=\"AUTOSTART\" value=\"" + autostart + " />";
                mp3Code += "<param name=\"SHUFFLE\" value=\"0\" />";
                mp3Code += "<param name=\"PREFETCH\" value=\"0\" />";
                mp3Code += "<param name=\"NOLABELS\" value=\"0\" />";
                mp3Code += "<param name=\"SRC\" value=\"" + src + "\" />";
                mp3Code += "<param name=\"CONTROLS\" value=\"StatusBar,ControlPanel\" />";
                mp3Code += "<param name=\"CONSOLE\" value=\"RAPLAYER\" />";
                mp3Code += "<param name=\"LOOP\" value=\"" + Convert.ToInt32(loop) + "\" />";
                mp3Code += "<param name=\"NUMLOOP\" value=\"0\" />";
                mp3Code += "<param name=\"CENTER\" value=\"0\" />";
                mp3Code += "<param name=\"MAINTAINASPECT\" value=\"0\" />";
                mp3Code += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
                mp3Code += "<param name=\"SCALE\" value=\"" + scale + "\" />";

                mp3Code += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                mp3Code += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                mp3Code += "quality=\"" + quality.ToString() + "\" ";
                mp3Code += "SCALE=\"" + scale + "\" ";
                mp3Code += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(autostart) + "\" autorewind=\"0\" playcount=\"0\" ";
                mp3Code += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                mp3Code += "filename=\"" + src + "\" />";

                mp3Code += "</object>";
                this.InnerXml = mp3Code;
            }

            XmlElement EmbedEle
            {
                get
                {
                    return (XmlElement)this.GetElementsByName("embed")[0];
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

            /*public string Hspace
            {
                get { return hspace; }
                set { hspace = value; }
            }

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
