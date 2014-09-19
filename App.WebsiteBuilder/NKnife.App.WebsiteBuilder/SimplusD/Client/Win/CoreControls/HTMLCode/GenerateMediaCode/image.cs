using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class image
    {
       /* public enum ImageUnit
        {
            percent,
            pix,
        }*/
        public enum ImageAlign
        {
            Default, BaseLine, Top,
            Middle, Bottom, TextTop,
            AbsoluteMiddle, AbsoluteBottom,
            Left, Right,
        }
        /// <summary>
        /// 生成图片的Html代码
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="path">路径</param>
        /// <param name="width">宽</param>
        /// <param name="widthUnit">宽单位</param>
        /// <param name="height">高</param>
        /// <param name="heightUnit">高单位</param>
        /// <param name="align">图片显示方式</param>
        /// <param name="border">边框</param>
        /// <param name="vspace"></param>
        /// <param name="hspace"></param>
        /// <param name="alt">说明文字</param>
        /// <param name="linkUrl">链接的URL</param>
        /// <param name="pic2Path">替换图片的路径</param>
        /// <param name="linkTarget">目标</param>
        /// <param name="linkTitle">标题</param>
        /// <param name="linkAccesskey">访问键</param>
        /// <returns></returns>
        public string ImageHtml(
            string name,
            string path,
            string width,
            string widthUnit,
            string height,
            string heightUnit,
            ImageAlign align,
            string border,
            string vspace,
            string hspace,
            string alt,
            string linkUrl,
            string pic2Path,
            string linkTarget,
            string linkTitle,
            string linkAccesskey,
            string mediaID
            )
        {
            string picMouseOut = "";
            string picMouseOver = "";
            string picCode = "";
            if (path == "")
                return "";

            //图片设置*/
            string imgHtml = "<img src=\"${srs_" + mediaID + "}\" ";
            imgHtml += "style = \"WIDTH:" + width + widthUnit + "; HEIGHT:" + height + heightUnit + "\" ";
            imgHtml += "border=\"" + border + "\" vspace=\"" + vspace + "\" ";
            imgHtml += "hspace=\"" + hspace + "\" alt=\"" + alt + "\" ";
            imgHtml += "name=\"" + name + "\" align=\"" + align.ToString().ToLower() + "\" />";

            //链接设置*/
            string linkHtml = "";
            if (linkUrl != "" || pic2Path != "")
            {
                if (linkUrl != "")
                {
                    linkHtml = "<a href=\"" + linkUrl + "\" ";
                    linkHtml += "target=\"" + linkTarget + "\" ";
                    linkHtml += "title=\"" + linkTitle + "\" ";
                    linkHtml += "accesskey=\"" + linkAccesskey + "\"";
                }
                else
                {
                    linkHtml = "<a href=\"#\"";
                }
                //图片替换
                if (pic2Path != "")
                {
                    picMouseOut = " na_restore_img_src('" + name + "', 'document')";
                    linkHtml += "OnMouseOut=\"" + picMouseOut + "\"";
                    picMouseOver = "na_change_img_src('" + name + "', 'document'," + pic2Path + ",true)";
                    linkHtml += "OnMouseOver=\"" + picMouseOver + "\"";
                }

                linkHtml += ">";
                picCode = linkHtml + imgHtml + @"</a>";
                return picCode;
            }
            else
            {
                picCode = picCode = imgHtml;
                return picCode;
            }
        }

    }
}
