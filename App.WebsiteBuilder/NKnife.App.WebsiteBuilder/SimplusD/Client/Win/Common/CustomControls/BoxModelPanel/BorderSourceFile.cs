using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static class BorderSourceFile
    {
        //TODO:路径还需修改
        static XmlDocument borderWidthDoc;
        static XmlDocument widthValueDoc;
        static XmlDocument borderMannerDoc;

        /// <summary>
        /// 存储窗体所需资源
        /// </summary>
        static public Dictionary<string, string> paddingWidthDic = new Dictionary<string, string>();
        static public Dictionary<string, string> borderWidthDic = new Dictionary<string, string>();
        static public Dictionary<string, string> borderUnitDic = new Dictionary<string, string>();
        static public Dictionary<string, string> borderStyleDic = new Dictionary<string, string>();

        static private string[] borderWidth;
        static private string[] paddingWidth;
        /// <summary>
        /// 存储边框宽度
        /// </summary>
        static public string[] BorderWidth
        {
            get
            {
                if (borderWidthDoc == null)
                {
                    borderWidthDoc = new XmlDocument();
                    if(!Service.Util.DesignMode)
                        borderWidthDoc.Load(PathService.CL_DS_BorderWidth);
                    borderWidth = ReadBorderWidth();
                }
                return borderWidth;
            }
        }

        static public string[] PaddingWidth
        {
            get
            {

                if (borderWidthDoc == null)
                {
                    borderWidthDoc = new XmlDocument();
                    if (!Service.Util.DesignMode)
                        borderWidthDoc.Load(PathService.CL_DS_BorderWidth); 
                    paddingWidth = ReadPaddingWidth();
                }
                return paddingWidth;
            }
        }

        static private string[] borderWidthUnit;
        /// <summary>
        /// 存储边框宽度值的单位
        /// </summary>
        static public string[] BorderWidthUnit
        {
            get
            {
                if (widthValueDoc == null)
                {
                    widthValueDoc = new XmlDocument();
                    if (!Service.Util.DesignMode)
                        widthValueDoc.Load(PathService.CL_DS_BorderWidthUnit);borderWidthUnit = ReadBorderWidthValue();
                }
                return borderWidthUnit;
            }
        }

        static private string[] borderStyle;
        /// <summary>
        /// 存储边框的样式
        /// </summary>
        static public string[] BorderStyle
        {
            get
            {
                if (borderMannerDoc == null)
                {
                    borderMannerDoc = new XmlDocument();
                    if (!Service.Util.DesignMode)
                        borderMannerDoc.Load(PathService.CL_DS_BorderStyle);
                    borderStyle = ReadBorderStyle();
                }
                return borderStyle;
            }
        }

        /// <summary>
        /// 读取边框的宽度
        /// </summary>
        static private string[] ReadBorderWidth()
        {
            int num = borderWidthDoc.DocumentElement.ChildNodes.Count;
            borderWidth = new string[num];
            num = 0;
            foreach (XmlNode node in borderWidthDoc.DocumentElement.ChildNodes)
            {
                //if (node.Attributes["name"].Value == "auto")
                //    continue;
                borderWidth[num++] = node.InnerText;
                borderWidthDic.Add(node.InnerText, node.Attributes["name"].Value);
            }
            return borderWidth;
        }

        static private string[] ReadPaddingWidth()
        {
            string w = "";
            foreach (XmlNode node in borderWidthDoc.DocumentElement.ChildNodes)
            {
                if (node.Attributes["name"].Value == "auto")
	            {
                    w = node.InnerText;
                    paddingWidthDic.Add(node.InnerText, node.Attributes["name"].Value);
                }
            }
            return new string[] { w };
        }

        /// <summary>
        /// 读取边框的宽度值
        /// </summary>
        static private string[] ReadBorderWidthValue()
        {
            int num = widthValueDoc.DocumentElement.ChildNodes.Count;
            borderWidthUnit = new string[num];
            num = 0;
            foreach (XmlNode node in widthValueDoc.DocumentElement.ChildNodes)
            {
                borderWidthUnit[num++] = node.InnerText;
                borderUnitDic.Add(node.InnerText, node.Attributes["name"].Value);
            }
            return borderWidthUnit;
        }
        /// <summary>
        /// 读取边框的样式
        /// </summary>
        static private string[] ReadBorderStyle()
        {
            int num = borderMannerDoc.DocumentElement.ChildNodes.Count;
            borderStyle = new string[num];
            num = 0;
            foreach (XmlNode node in borderMannerDoc.DocumentElement.ChildNodes)
            {
                borderStyle[num++] = node.InnerText;
                borderStyleDic.Add(node.InnerText, node.Attributes["name"].Value);
            }
            return BorderStyle;
        }
    }
}
