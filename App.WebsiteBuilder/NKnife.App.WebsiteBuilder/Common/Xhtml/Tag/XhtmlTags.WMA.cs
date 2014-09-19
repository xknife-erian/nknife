using System;
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
        public class WMA : XhtmlTags.Object
        {
            internal WMA() { }

            //<IMG style="WIDTH: 500px; HEIGHT: 667px" alt="" hspace=11 
            //src="${srs_d43bab22b0a2469287257db914d95dd4}" align=baseline vspace=22 border=33 name=pic>

            public void Builder(CssSection style, string hspace, string src, Xhtml.Align align, string quality, int loop, int autostart, string vspace, string border, string title,string scale)
            {
                string width = style.Properties["width"];
                string height = style.Properties["height"];
                string audioCode = "";
                audioCode = "<object style=\"WIDTH: " + width + "; HEIGHT: " + height + " \"";
                audioCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                audioCode += "type='application/x-oleobject\"'";// height=\"115\" width=\"220\"";
                audioCode += "classid=\"CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6\">";
                audioCode += "<param name=\"URL\" value=\"" + src + "\" />";
                audioCode += "<param name=\"rate\" value=\"1\" />";
                audioCode += "<param name=\"balance\" value=\"0\" />";
                audioCode += "<param name=\"currentPosition\" value=\"0\" />";
                audioCode += "<param name=\"defaultFrame\" value=\"\" />";
                audioCode += "<param name=\"playCount\" value=\"100\" />";
                audioCode += "<param name=\"autoStart\" value=\"" + Convert.ToInt32(autostart) + "\" />";
                audioCode += "<param name=\"currentMarker\" value=\"0\" />";
                audioCode += "<param name=\"invokeURLs\" value=\"-1\" />";
                audioCode += "<param name=\"baseURL\" value=\"\" />";
                audioCode += "<param name=\"volume\" value=\"100\" />";
                audioCode += "<param name=\"mute\" value=\"0\" />";
                audioCode += "<param name=\"uiMode\" value=\"full\" />";
                audioCode += "<param name=\"stretchToFit\" value=\"0\" />";
                audioCode += "<param name=\"windowlessVideo\" value=\"0\" />";
                audioCode += "<param name=\"enabled\" value=\"-1\" />";
                audioCode += "<param name=\"enableContextMenu\" value=\"0\" />";
                audioCode += "<param name=\"fullScreen\" value=\"0\" />";
                audioCode += "<param name=\"SAMIStyle\" value=\"\" />";
                audioCode += "<param name=\"SAMILang\" value=\"\" />";
                audioCode += "<param name=\"SAMIFilename\" value=\"\" />";
                audioCode += "<param name=\"captioningID\" value=\"\" />";
                audioCode += "<param name=\"enableErrorDialogs\" value=\"0\" />";
                audioCode += "<param name=\"_cx\" value=\"5821\" />";
                audioCode += "<param name=\"_cy\" value=\"3043\" />";
                audioCode += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
                audioCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";


                audioCode += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                audioCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                audioCode += "quality=\"" + quality.ToString() + "\" ";
                audioCode += "SCALE=\"" + scale + "\" ";
                audioCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(autostart) + "\" autorewind=\"0\" playcount=\"0\" ";
                audioCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                audioCode += "filename=\"" + src + "\" />";

                audioCode += "</object>";
                this.InnerXml = audioCode;
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
