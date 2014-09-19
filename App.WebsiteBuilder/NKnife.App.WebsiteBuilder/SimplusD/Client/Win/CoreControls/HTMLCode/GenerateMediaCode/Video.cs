using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class Video
    {
        string classid_Media ="clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA";
       // string type_Media = "audio/x-pn-RealAudio-plugin";

        public enum Align
        {
            Default,
            baseline,
            top,
            middle,
            bottom,
            texttop,
            absolutemiddle,
            absolutebottom,
            left,
            right,
        }

        public enum Quality
        { 
            Hight,
            Low,
            AutoHight,
            AutoLow,
        }
        /// <summary>
        /// 生成视频的Html的代码
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="vspace"></param>
        /// <param name="hspace"></param>
        /// <param name="title">标题</param>
        /// <param name="accessKey">访问键</param>
        /// <param name="tabIndex">索引</param>
        /// <param name="align">显示方式</param>
        /// <param name="quality">显示质量</param>
        /// <param name="isLoopPlay">循环播放</param>
        /// <param name="isAutoPlay">自动播放</param>
        /// <returns>返回字符串</returns>
        public string MediaHtml(
            string path,
            string width,
            string height,
            string vspace,
            string hspace,
            string title,
            string accessKey,
            string tabIndex,
            Align align,
            Quality quality,
            bool isLoopPlay,
            bool isAutoPlay,//自动播放
            string scale,
            string mediaID
            )
        {
            string mediaCode = "";
            string strExtension = Path.GetExtension(path);
            switch (strExtension)
            {
                case ".rm":
                case ".rmvb":
                    {
                        mediaCode = "<object id=\"player\" height=\"" + height + "\" width=\"" + width + "\"";
                        mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        mediaCode += "classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" name=\"player\">";
                        mediaCode += "<param name=\"_ExtentX\" value=\"10583\" />";
                        mediaCode += "<param name=\"_ExtentY\" value=\"7197\" />";
                        mediaCode += "<param name=\"AUTOSTART\" value=\"" + (0 - Convert.ToInt32(isAutoPlay)) + "\" />";
                        mediaCode += "<param name=\"SHUFFLE\" value=\"0\" />";
                        mediaCode += "<param name=\"PREFETCH\" value=\"0\" />";
                        mediaCode += "<param name=\"NOLABELS\" value=\"-1\" />";
                        mediaCode += "<param name=\"SRC\" value=\"" + "${srs_" + mediaID + "}" + "\" />";
                        mediaCode += "<param name=\"CONTROLS\" value=\"Imagewindow\" />";
                        mediaCode += "<param name=\"CONSOLE\" value=\"clip1\" />";
                        mediaCode += "<param name=\"LOOP\" value=\"" + Convert.ToInt32(isLoopPlay) + "\" />";
                        mediaCode += "<param name=\"NUMLOOP\" value=\"0\" />";
                        mediaCode += "<param name=\"CENTER\" value=\"0\" />";
                        mediaCode += "<param name=\"MAINTAINASPECT\" value=\"0\" />";
                        mediaCode += "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\" />";
                        mediaCode += "<param name=\"quality\" value=\""+quality.ToString()+"\" />";
                        mediaCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";
                        

                        mediaCode += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                        mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        mediaCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(isAutoPlay) + "\" autorewind=\"0\" playcount=\"0\" ";
                        mediaCode += "moviewindowheight=\""+ height + "\" moviewindowwidth=\"" + width + "\" ";
                        mediaCode += "filename=\"" + "${srs_" + mediaID + "}" + "\" />";


                        mediaCode += "</object>";
                        return mediaCode;
                    }
                case ".avi":
                    {
                        mediaCode = "<object id=\"video1\" height=\""+height+"\" width=\""+width+"\"";
                        mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        mediaCode += "classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">";
                        mediaCode += "<param name=\"_ExtentX\" value=\"9313\" />";
                        mediaCode += "<param name=\"_ExtentY\" value=\"7620\" />";
                        mediaCode += "<param name=\"AUTOSTART\" value=\"" + (0 - Convert.ToInt32(isAutoPlay)) + "\" />";
                        mediaCode += "<param name=\"SHUFFLE\" value=\"0\" />";
                        mediaCode += "<param name=\"PREFETCH\" value=\"0\" />";
                        mediaCode += "<param name=\"NOLABELS\" value=\"0\" />";
                        mediaCode += "<param name=\"SRC\" value=\"" + "${srs_" + mediaID + "}" + "\" />";
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
                        mediaCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(isAutoPlay) + "\" autorewind=\"0\" playcount=\"0\" ";
                        mediaCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                        mediaCode += "filename=\"" + "${srs_" + mediaID + "}" + "\" />";

                        mediaCode += "</object>";
                        return mediaCode;
                    }
                //case ".mpg":
                //    return MediaObject.GetMediaPlayerObjectString(width, height, (MediaObject.Align)align, mediaID, isLoopPlay, isAutoPlay);
                case ".wmv":
                    {
                        mediaCode = "<object id=\"mPlayer1\" style=\"WIDTH: "+width+"; HEIGHT: "+height+"\"";
                        mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        mediaCode += "type='application/x-oleobject\"' height=\"115\" width=\"220\"";
                        mediaCode += "classid=\"CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6\">";
                        mediaCode += "<param name=\"URL\" value=\"" + "${srs_" + mediaID + "}" + "\" />";
                        mediaCode += "<param name=\"rate\" value=\"1\" />";
                        mediaCode += "<param name=\"balance\" value=\"0\" />";
                        mediaCode += "<param name=\"currentPosition\" value=\"0\" />";
                        mediaCode += "<param name=\"defaultFrame\" value=\"\" />";
                        mediaCode += "<param name=\"playCount\" value=\"100\" />";
                        mediaCode += "<param name=\"autoStart\" value=\""+(0-Convert.ToInt32(isAutoPlay))+"\" />";
                        mediaCode += "<param name=\"currentMarker\" value=\"0\" />";
                        mediaCode += "<param name=\"invokeURLs\" value=\"-1\" />";
                        mediaCode += "<param name=\"baseURL\" value=\"\" />";
                        mediaCode += "<param name=\"volume\" value=\"100\" />";
                        mediaCode += "<param name=\"mute\" value=\"0\" />";
                        mediaCode += "<param name=\"uiMode\" value=\"full\" />";
                        mediaCode += "<param name=\"stretchToFit\" value=\"0\" />";
                        mediaCode += "<param name=\"windowlessVideo\" value=\"0\" />";
                        mediaCode += "<param name=\"enabled\" value=\"-1\" />";
                        mediaCode += "<param name=\"enableContextMenu\" value=\"0\" />";
                        mediaCode += "<param name=\"fullScreen\" value=\"0\" />";
                        mediaCode += "<param name=\"SAMIStyle\" value=\"\" />";
                        mediaCode += "<param name=\"SAMILang\" value=\"\" />";
                        mediaCode += "<param name=\"SAMIFilename\" value=\"\" />";
                        mediaCode += "<param name=\"captioningID\" value=\"\" />";
                        mediaCode += "<param name=\"enableErrorDialogs\" value=\"0\" />";
                        mediaCode += "<param name=\"_cx\" value=\"5821\" />";
                        mediaCode += "<param name=\"_cy\" value=\"3043\" />";
                        mediaCode += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
                        mediaCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";

                        mediaCode += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                        mediaCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        mediaCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(isAutoPlay) + "\" autorewind=\"0\" playcount=\"0\" ";
                        mediaCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                        mediaCode += "filename=\"" + "${srs_" + mediaID + "}" + "\" />";

                        mediaCode += "</object>";

                        return mediaCode;
                    }
                default:
                    {
                        return "";
                    }
            }
        }
       

        private void media_Align(XmlElement ele, Align medAlign)
        {
            switch (medAlign)
            {
                case Align.Default:
                    break;
                case Align.baseline:
                    ele.SetAttribute("align", "baseline");
                    break;
                case Align.top:
                    ele.SetAttribute("align", "top");
                    break;
                case Align.middle:
                    ele.SetAttribute("align", "middle");
                    break;
                case Align.bottom:
                    ele.SetAttribute("align", "bottom");
                    break;
                case Align.texttop:
                    ele.SetAttribute("align", "texttop");
                    break;
                case Align.absolutemiddle:
                    ele.SetAttribute("align", "absmiddle");
                    break;
                case Align.absolutebottom:
                    ele.SetAttribute("align", "left");
                    break;
                case Align.left:
                    break;
                case Align.right:
                    break;
                default:
                    break;
            }

        }


    }
}
