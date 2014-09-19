using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public class Table
    {
        public enum TableUnit
        {
            percent,
            pix,
        }

        public enum HeadScope
        { 
            none,
            left,
            top,
            both,
        }

        /// <summary>
        /// 生成表格的Html代码
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="width">宽</param>
        /// <param name="unit">表格宽度的单位</param>
        /// <param name="borderWidth">边宽</param>
        /// <param name="cellSpac">边距</param>
        /// <param name="cellPadd">间距</param>
        /// <param name="scope">表格头显示方式</param>
        /// <param name="caption">标题</param>
        /// <param name="align">标题的显示方式</param>
        /// <param name="summary">摘要</param>
        /// <returns></returns>
        public string TableHtml(
            string rows,
            string cols,
            string width,
            TableUnit unit,
            string borderWidth,
            string cellSpac,
            string cellPadd,
            HeadScope scope,
            string caption,
            string align, 
            string summary
            
            )
        {
            string tableCode = "";

            XmlDocument doc = new XmlDocument();
            XmlElement ele = doc.CreateElement("table");

            if (width  != "")
            {
                switch (unit)
                {
                    case TableUnit.percent:
                        ele.SetAttribute("width", width + "%");
                        break;
                    case TableUnit.pix:
                        ele.SetAttribute("width", width);
                        break;
                    default:
                        break;
                }
            }
            if (borderWidth != "" && borderWidth != "0")
                ele.SetAttribute("border", borderWidth);
            else if (borderWidth == "0")
            {
                ele.SetAttribute("Style", "border:Dashed 3px");// System.Web.UI.WebControls.BorderStyle.Dotted.ToString());
                ele.SetAttribute("border", "1");
            }
            if (cellSpac != "")
                ele.SetAttribute("cellsapcing", cellSpac);
            if (cellPadd != "")
                ele.SetAttribute("cellpadding", cellPadd);
            if (summary != "")
                ele.SetAttribute("summary", summary);
            if (caption != "")
            {
                XmlElement eleCap = doc.CreateElement("caption");
                eleCap.SetAttribute("align", align);
                eleCap.InnerText = caption;
                ele.AppendChild(eleCap);
            }
            for (int r = 0; r < Convert.ToInt32 (rows); r++)
            {
                XmlElement eleTR = doc.CreateElement("tr");
                for (int c = 0; c < Convert.ToInt32(cols); c++)
                {
                    XmlElement eleTD = doc.CreateElement("td");
                    switch (scope)
                    {
                        case HeadScope.none:
                            break;
                        case HeadScope.left:
                            if (c == 0)
                                eleTD.SetAttribute("scope", "row");
                            break;
                        case HeadScope.top:
                            if (r == 0)
                                eleTD.SetAttribute("scope", "col");
                            break;
                        case HeadScope.both:
                            if (r == 0)
                            {
                                eleTD.SetAttribute("scope", "col");
                            }
                            else
                            {
                                if (c == 0)
                                {
                                    eleTD.SetAttribute("scope", "row");
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //eleTD.InnerXml = "&nbsp;";
                    eleTR.AppendChild(eleTD );
                }
                ele.AppendChild(eleTR);
            }
            XmlNode tableNode = doc.AppendChild(ele);
            tableCode = tableNode.OuterXml;
            return tableCode;
        }
    }
}
