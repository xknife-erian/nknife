using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.Win
{
    public class Flash
    {
        string classid_flash = "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
        string codebase_flash = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0";
        string pluginspage_flash = "http://www.macromedia.com/go/getflashplayer";
        string type_flash = "application/x-shockwave-flash";

        /// <summary>
        /// 生成Flash的Html的代码
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
        public string FlashHtml(
            InsertMode insertMode,
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
            string flashCode = "";
            string pathStr = "";
            if (insertMode == InsertMode.Id)
            {
                pathStr = "${srs_" + mediaID + "}";
            }
            else
                pathStr = path;
            XmlDocument flashDoc = new XmlDocument();

            flashCode = "<noscript>";
            flashCode += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"  ";
            flashCode += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" ";
            flashCode += "style=\"WIDTH: " + width + "; HEIGHT: " + height + "\" hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
            flashCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
            flashCode += "title=\"" + title + "\" accesskey=\"" + accessKey + "\" tabindex=\"" + tabIndex + "\" align=\"" + align + "\">";
            flashCode += "<param name=\"movie\" value=\""+pathStr+"\" />";
            
            flashCode += "<param name=\"loop\" value=\"" + Convert.ToInt32(isLoopPlay) + "\" />";
            flashCode += "<param name=\"play\" value=\"" + Convert.ToInt32(isAutoPlay) + "\" />";
            flashCode += "<param name=\"scale\" value=\"" + scale + "\" />";
            flashCode += "<param name=\"src\" value=\"${srs_" + mediaID + "}\" />";
            flashCode += "<param name=\"quality\" value=\"" + quality.ToString() + "\" />";
            flashCode += "<param name=\"SCALE\" value=\"" + scale + "\" />";

            flashCode += "<embed width=\"" + width + "\" height=\"" + height + "\" ";
            flashCode += "src=\"${srs_" + mediaID + "}\" ";
            flashCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\"";
            flashCode += "align=\"" + align.ToString() + "\" quality=" + quality.ToString() + " ";
            flashCode += "pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" ";
            flashCode += "scale=\"" + scale + "\"></embed>";

            flashCode += "</object>";
            flashCode += "</noscript>";


            return flashCode;
        }
        
        private void flash_Align(XmlElement ele, Align flaAlign)
        {
            switch (flaAlign)
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
