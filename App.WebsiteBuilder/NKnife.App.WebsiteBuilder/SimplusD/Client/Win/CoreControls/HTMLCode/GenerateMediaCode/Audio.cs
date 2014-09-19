using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class Audio
    {
        string classid_Audio = "clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA";//默认为realone
        string codebase_Audio = "http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715";
        //string pluginspage_Audio = "http://www.microsoft.com/windows/windowsmedia/download/";
        string type_Audio = "";
        string standby_Audio = "";
        string classid_wma = "{id}";
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
        /// 生成音频的Html的代码
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
        public string AudioHtml(
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
            bool isAutoPlay,
            string scale,
            string mediaID
            )
        {
            string audioCode = "";

            string strExtension = Path.GetExtension(path);
            switch (strExtension)
            {
                case ".mp3":
                    {
                        audioCode = "<object style=\"WIDTH: " + width + "; HEIGHT: "+height+" \"";
                        audioCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        audioCode += "classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">";
                        audioCode += "<param name=\"_ExtentX\" value=\"8599\" /> ";
                        audioCode += "<param name=\"_ExtentY\" value=\"3916\" />";
                        audioCode += "<param name=\"AUTOSTART\" value=\""+Convert.ToInt32(isAutoPlay)+"\" />";
                        audioCode += "<param name=\"SHUFFLE\" value=\"0\" />";
                        audioCode += "<param name=\"PREFETCH\" value=\"0\" />";
                        audioCode += "<param name=\"NOLABELS\" value=\"0\" />";
                        audioCode += "<param name=\"SRC\" value=\"" + "${srs_" + mediaID + "}" + "\" />";
                        audioCode += "<param name=\"CONTROLS\" value=\"StatusBar,ControlPanel\" />";
                        audioCode += "<param name=\"CONSOLE\" value=\"RAPLAYER\" />";
                        audioCode += "<param name=\"LOOP\" value=\"" +Convert.ToInt32(isLoopPlay)+ "\" />";
                        audioCode += "<param name=\"NUMLOOP\" value=\"0\" />";
                        audioCode += "<param name=\"CENTER\" value=\"0\" />";
                        audioCode += "<param name=\"MAINTAINASPECT\" value=\"0\" />";
                        audioCode += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
                        audioCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";

                        audioCode += "<embed width=\"" + width + "\" height=\"" + height + "\" border=\"0\" showdisplay=\"0\" ";
                        audioCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        audioCode += "quality=\"" + quality.ToString() + "\" ";
                        audioCode += "SCALE=\"" + scale + "\" ";
                        audioCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(isAutoPlay) + "\" autorewind=\"0\" playcount=\"0\" ";
                        audioCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                        audioCode += "filename=\"" + "${srs_" + mediaID + "}" + "\" />";

                        audioCode += "</object>";
                        return audioCode;
                    }
                case ".wma":
                    {
                        audioCode = "<object style=\"WIDTH: " + width + "; HEIGHT: " + height + " \"";
                        audioCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
                        audioCode += "type='application/x-oleobject\"'";// height=\"115\" width=\"220\"";
                        audioCode += "classid=\"CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6\">";
                        audioCode += "<param name=\"URL\" value=\"" + "${srs_" + mediaID + "}" + "\" />";
                        audioCode += "<param name=\"rate\" value=\"1\" />";
                        audioCode += "<param name=\"balance\" value=\"0\" />";
                        audioCode += "<param name=\"currentPosition\" value=\"0\" />";
                        audioCode += "<param name=\"defaultFrame\" value=\"\" />";
                        audioCode += "<param name=\"playCount\" value=\"100\" />";
                        audioCode += "<param name=\"autoStart\" value=\""+Convert.ToInt32(isAutoPlay)+"\" />";
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
                        audioCode += "showcontrols=\"1\" autostart=\"" + Convert.ToInt32(isAutoPlay) + "\" autorewind=\"0\" playcount=\"0\" ";
                        audioCode += "moviewindowheight=\"" + height + "\" moviewindowwidth=\"" + width + "\" ";
                        audioCode += "filename=\"" + "${srs_" + mediaID + "}" + "\" />";

                        audioCode += "</object>";

                        return audioCode;
                    }
                default:
                    {
                        return "";
                    }
            }
        }
        private void Audio_Align(XmlElement ele, Align audioAlign)
        {
            switch (audioAlign)
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
