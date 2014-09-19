using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class Flash
    {
        string classid_flash = "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
        string codebase_flash = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0";
        string pluginspage_flash = "http://www.macromedia.com/go/getflashplayer";
        string type_flash = "application/x-shockwave-flash";

        public enum FlashAlign
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

        public enum FlashQuality
        { 
            Hight,
            Low,
            AutoHight,
            AutoLow,
        }
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
            string path,
            string width,
            string height,
            string vspace,
            string hspace,
            string title,
            string accessKey,
            string tabIndex,
            FlashAlign align,
            FlashQuality quality,
            bool isLoopPlay,
            bool isAutoPlay,
            string scale,
            string mediaID
            )
        {
            string flashCode = "";
            XmlDocument flashDoc = new XmlDocument();

            flashCode = "<noscript>";
            flashCode += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"  ";
            flashCode += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" ";
            flashCode += "style=\"WIDTH: " + width + "; HEIGHT: " + height + "\" hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
            flashCode += "hspace=\"" + hspace + "\" vspace=\"" + vspace + "\" ";
            flashCode += "title=\"" + title + "\" accesskey=\"" + accessKey + "\" tabindex=\"" + tabIndex + "\" align=\"" + align + "\">";
            flashCode += "<param name=\"movie\" value=\"${srs_" + mediaID + "}\" />";
            
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
            //XmlElement rootEle = flashDoc.CreateElement("noscript");

            //XmlElement objectEle = flashDoc.CreateElement("object");
            ////objectEle.SetAttribute("classid", classid_flash);
            ////objectEle.SetAttribute("codebase", codebase_flash);

            ////XmlElement embedEle = flashDoc.CreateElement("embed");
            ////embedEle.SetAttribute("src", "${srs_" + mediaID + "}");
            //if (width != "")
            //{
            //    //objectEle.SetAttribute("width",width );
            //    //embedEle.SetAttribute("width",width );
            //}
            //if (height != "")
            //{
            //    //objectEle.SetAttribute("height", height );
            //    //embedEle.SetAttribute("height", height);
            //}
            //if (hspace  != "")
            //    objectEle.SetAttribute("hspace", hspace );
            //if (vspace  != "")
            //    objectEle.SetAttribute("vspace", vspace);
            //if (title != "")
            //    objectEle.SetAttribute("title", title);
            //if (accessKey  != "")
            //    objectEle.SetAttribute("accesskey", accessKey);
            //if (tabIndex != "")
            //    objectEle.SetAttribute("tabindex", tabIndex);
                  
            //图片显示位置
            //flash_Align(objectEle, align);

            //XmlElement pathEle = flashDoc.CreateElement("param");
            //pathEle.SetAttribute("name", "movie");
            //pathEle.SetAttribute("value", path);
            //objectEle.AppendChild(pathEle);

        //    //图片显示质量
        //    if (quality != FlashQuality.AutoHight)
        //    {
        //        XmlElement qualityEle = flashDoc.CreateElement("param");
        //        qualityEle.SetAttribute("name", "quality");
        //        switch (quality)
        //        {
        //            case FlashQuality.AutoHight:
        //                break;
        //            case FlashQuality.Low:
        //                qualityEle.SetAttribute("value", "low");
        //                //embedEle.SetAttribute("quality", "low");
        //                break;
        //            case FlashQuality.Hight:
        //                qualityEle.SetAttribute("value", "high");
        //                //embedEle.SetAttribute("quality", "high");
        //                break;
        //            case FlashQuality.AutoLow:
        //                qualityEle.SetAttribute("value", "autolow");
        //               // embedEle.SetAttribute("quality", "autolow");
        //                break;
        //            default:
        //                break;
        //        }
        //        objectEle.AppendChild(qualityEle);
        //    }
        //    //循环播放
        //    if (isLoopPlay)
        //    {
        //        XmlElement isLoopPlayEle = flashDoc.CreateElement("param");
        //        isLoopPlayEle.SetAttribute("name", "loop");
        //        isLoopPlayEle.SetAttribute("value", "true");
        //        objectEle.AppendChild(isLoopPlayEle);
        //        //embedEle.SetAttribute("loop", "true");
        //    }
        //    //自动播放
        //    if (isAutoPlay)
        //    {
        //        XmlElement isAutoPlayEle = flashDoc.CreateElement("param");
        //        isAutoPlayEle.SetAttribute("name", "play");
        //        isAutoPlayEle.SetAttribute("value", "true");
        //        objectEle.AppendChild(isAutoPlayEle);
        //        //embedEle.SetAttribute("play", "true");
        //    }
        //   //自动播放
        //    if (scale!="")
        //    {
        //        XmlElement scaleEle = flashDoc.CreateElement("param");
        //        scaleEle.SetAttribute("name", "scale");
        //        scaleEle.SetAttribute("value", scale);
        //        objectEle.AppendChild(scaleEle);
        //        //embedEle.SetAttribute("scale", scale);
        //    }

        //    XmlElement srcEle = flashDoc.CreateElement("param");
        //    srcEle.SetAttribute("name", "src");
        //    srcEle.SetAttribute("value", "${srs_" + mediaID + "}");
        //    objectEle.AppendChild(srcEle);
            
        //    //embedEle.SetAttribute("pluginspage", pluginspage_flash);
        //    //embedEle.SetAttribute("type", type_flash);
        //    //objectEle.AppendChild(embedEle);

        //    rootEle.AppendChild(objectEle);
        //    XmlNode flashNode = flashDoc.AppendChild(rootEle);
        //    flashCode = flashNode.OuterXml;
        //    return flashCode;
        

        private void flash_Align(XmlElement ele, FlashAlign flaAlign)
        {
            switch (flaAlign)
            {
                case FlashAlign.Default:
                    break;
                case FlashAlign.baseline:
                    ele.SetAttribute("align", "baseline");
                    break;
                case FlashAlign.top:
                    ele.SetAttribute("align", "top");
                    break;
                case FlashAlign.middle:
                    ele.SetAttribute("align", "middle");
                    break;
                case FlashAlign.bottom:
                    ele.SetAttribute("align", "bottom");
                    break;
                case FlashAlign.texttop:
                    ele.SetAttribute("align", "texttop");
                    break;
                case FlashAlign.absolutemiddle:
                    ele.SetAttribute("align", "absmiddle");
                    break;
                case FlashAlign.absolutebottom:
                    ele.SetAttribute("align", "left");
                    break;
                case FlashAlign.left:
                    break;
                case FlashAlign.right:
                    break;
                default:
                    break;
            }

        }
    }
}
