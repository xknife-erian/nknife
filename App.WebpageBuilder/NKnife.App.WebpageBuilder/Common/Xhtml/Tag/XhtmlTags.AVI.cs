﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu
{
    public partial class XhtmlTags
    {
//<OBJECT style="WIDTH: 100px; HEIGHT: 100px" type='application/x-oleobject"' 
//        hspace=3 vspace=4 classid=CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6>
//        <PARAM NAME="URL" VALUE="${srs_d329f654c43041ceb2579558ea27ef60}">
//<PARAM NAME="rate" VALUE="1">
//<PARAM NAME="balance" VALUE="0">
//<PARAM NAME="currentPosition" VALUE="0">
//<PARAM NAME="defaultFrame" VALUE="">
//<PARAM NAME="playCount" VALUE="100">
//<PARAM NAME="autoStart" VALUE="-1">
//<PARAM NAME="currentMarker" VALUE="0">
//<PARAM NAME="invokeURLs" VALUE="-1">
//<PARAM NAME="baseURL" VALUE="">
//<PARAM NAME="volume" VALUE="100">
//<PARAM NAME="mute" VALUE="0">
//<PARAM NAME="uiMode" VALUE="full">
//<PARAM NAME="stretchToFit" VALUE="0">
//<PARAM NAME="windowlessVideo" VALUE="0">
//<PARAM NAME="enabled" VALUE="-1">
//<PARAM NAME="enableContextMenu" VALUE="0">
//<PARAM NAME="fullScreen" VALUE="0">
//<PARAM NAME="SAMIStyle" VALUE="">
//<PARAM NAME="SAMILang" VALUE="">
//<PARAM NAME="SAMIFilename" VALUE="">
//<PARAM NAME="captioningID" VALUE="">
//<PARAM NAME="enableErrorDialogs" VALUE="0">
//<PARAM NAME="_cx" VALUE="2646">
//<PARAM NAME="_cy" VALUE="2646">
//<embed width="100px" height="100px" border="0" showdisplay="0" hspace="3" 
//        vspace="4" quality="AutoHight" SCALE="noborder" showcontrols="1" autostart="1" 
//        autorewind="0" playcount="0" moviewindowheight="100px" moviewindowwidth="100px" 
//        filename="${srs_d329f654c43041ceb2579558ea27ef60}" /></OBJECT>
        public class AVI : XhtmlTags.Object
        {
            internal AVI() { }

            //<IMG style="WIDTH: 500px; HEIGHT: 667px" alt="" hspace=11 
            //src="${srs_d43bab22b0a2469287257db914d95dd4}" align=baseline vspace=22 border=33 name=pic>

            public void Builder(CssSection style, string hspace, string src, Xhtml.Align align, string quality, int loop, int autostart, string vspace, string border, string title, string scale)
            {
                string width = style.Properties["width"];
                string height = style.Properties["height"];
                string mediaCode = "";
                mediaCode = "<object id=\"video1\" height=\"" + height + "\" width=\"" + width + "\"";
                mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                mediaCode += "classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">";
                mediaCode += "<param name=\"_ExtentX\" value=\"9313\" />";
                mediaCode += "<param name=\"_ExtentY\" value=\"7620\" />";
                mediaCode += "<param name=\"AUTOSTART\" value=\"" + (0 - Convert.ToInt32(autostart)) + "\" />";
                mediaCode += "<param name=\"SHUFFLE\" value=\"0\" />";
                mediaCode += "<param name=\"PREFETCH\" value=\"0\" />";
                mediaCode += "<param name=\"NOLABELS\" value=\"0\" />";
                mediaCode += "<param name=\"SRC\" value=\"" + src+ "\" />";
                mediaCode += "<param name=\"CONTROLS\" value=\"ImageWindow\" />";
                mediaCode += "<param name=\"CONSOLE\" value=\"Clip1\" />";
                mediaCode += "<param name=\"LOOP\" value=\"0\" />";
                mediaCode += "<param name=\"NUMLOOP\" value=\"0\" />";
                mediaCode += "<param name=\"CENTER\" value=\"0\" />";
                mediaCode += "<param name=\"MAINTAINASPECT\" value=\"0\" />";
                mediaCode += "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\" />";
                mediaCode += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
                mediaCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";


                mediaCode += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                mediaCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(autostart) + "\" autorewind=\"0\" playcount=\"0\" ";
                mediaCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                mediaCode += "filename=\"" + src + "\" />";

                mediaCode += "</object>";
                this.InnerXml = mediaCode;
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

           /* public string Hspace
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
