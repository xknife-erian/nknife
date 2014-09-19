using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using TidyNet;
using System.Drawing;
using System.Windows.Forms;
using mshtml;
using ICSharpCode.TextEditor;
using System.Drawing.Drawing2D;
using TidyNet;

namespace Jeelu.SimplusD.Client.Win
{
    public class GeneralMethods
    {
        public static string tidy(string str)
        {
            Tidy tidy = new Tidy();
            TidyMessageCollection msg = new TidyMessageCollection();

            MemoryStream input = new MemoryStream();
            MemoryStream output = new MemoryStream();

            tidy.Options.CharEncoding = CharEncoding.UTF8;
            tidy.Options.DocType = DocType.Strict;
            tidy.Options.DropFontTags = true;
            tidy.Options.LogicalEmphasis = true;
            tidy.Options.Xhtml = true;
            tidy.Options.MakeClean = true;
            tidy.Options.TidyMark = true;
            tidy.Options.TabSize = 0;

            if (str != null)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(str);
                input.Write(byteArray, 0, byteArray.Length);
                input.Position = 0;

                tidy.Parse(input, output, msg);//粘贴的Word文档在此有出现代码丢失的情况
                string outputString = Encoding.UTF8.GetString(output.ToArray());

                int bodybegin = outputString.IndexOf("<body>");
                int bodyend = outputString.IndexOf("</body>");
                if (bodybegin > 0 && bodyend > 0)
                {
                    int length = bodyend - bodybegin - 10;
                    if (length < 0)
                        length = 0;
                    string realoutputString = outputString.Substring(bodybegin +8, length);
                    return realoutputString;
                }
                else
                    return "";
            }
            else
                return "";
        }

        public static void tidySpan(IHTMLDocument2 idoc2)
        {
            IHTMLElementCollection elements = (IHTMLElementCollection)(idoc2).all;
            string pstylestr = "";
            string estylestr = "";
            string pinnstr = "";
            string einnstr = "";
            IHTMLElement pele = null;
            IHTMLElement eele = null;
            foreach (IHTMLElement element in elements)
            {
                if (element.tagName == "SPAN")
                {
                    string outstr = element.outerHTML;
                    pele = eele;
                    eele = element;
                    pinnstr = einnstr;
                    einnstr = element.innerText;
                    pstylestr = estylestr;
                    if (outstr.IndexOf("style") > 0)
                    {
                        estylestr = outstr.Substring(outstr.IndexOf("style"), outstr.IndexOf(">") - outstr.IndexOf("style"));
                        if (estylestr == pstylestr)
                        {
                            eele.innerText = pinnstr + element.innerText;
                            pele.innerText = "";
                            einnstr = eele.innerText;
                        }
                    }
                    else
                    {
                        eele.outerHTML = eele.innerHTML;
                    }
                }
            }
        }

        public static string toXML(string inputstr, string typ)
        {
            MemoryStream input = new MemoryStream();
            MemoryStream output = new MemoryStream();
            //Tidy tidy = new Tidy();
            //tidy.Options.XmlOut = true;
            //TidyMessageCollection msg = new TidyMessageCollection();

            byte[] byteArray = Encoding.UTF8.GetBytes(inputstr);
            input.Write(byteArray, 0, byteArray.Length);
            input.Position = 0;

            //tidy.Parse(input, output, msg);
            string outputString = Encoding.UTF8.GetString(output.ToArray());

            int objectbegin = outputString.IndexOf("<object");
            int objectend = outputString.IndexOf("</object>");
            if (objectbegin > 0 && objectend > 0)
            {
                string objectstr = outputString.Substring(objectbegin, objectend - objectbegin + 9);
                outputString = outputString.Remove(objectbegin, objectend - objectbegin + 9);
                outputString = outputString.Insert(outputString.IndexOf("</html>"), "<body>" + objectstr + "</body>");
            }

            int bodybegin = outputString.IndexOf("<body>");
            int bodyend = outputString.IndexOf("</body>");
            if (bodybegin > 0 && bodyend > 0)
            {
                string realoutputString = "";
                if (typ == "img")
                    realoutputString = outputString.Substring(bodybegin + 6, bodyend - bodybegin - 12);
                else if (typ == "hr")
                    realoutputString = outputString.Substring(bodybegin + 6, bodyend - bodybegin - 6);
                else if (typ == "flash")
                    realoutputString = outputString.Substring(bodybegin + 6, bodyend - bodybegin - 6);
                else
                    realoutputString = outputString.Substring(bodybegin + 6, bodyend - bodybegin - 6);
                return realoutputString;
            }
            else return "";
        }

        public static string ColorToRGB(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        /// <summary>
        /// HTML编辑器编辑模式切换
        /// </summary>
        /// <param name="htmlPanel"></param>
        /// <param name="type"></param>
        /// <param name="openMode">0为打开文件时，1为点击按钮切换时</param>
        public static void SetForModeChage(HTMLDesignControl htmlPanel, DesignerOpenType type,int openMode)
        {
            int designPanelHeight = Service.Sdsite.DesignDataDocument.HTMLDesignerDesignPanelHeight;
            if (openMode == 1)
                Service.Sdsite.DesignDataDocument.HTMLDesignerDesignPanelHeight = htmlPanel.splitCon.SplitterDistance;

            switch (type)
            {
                case DesignerOpenType.Design:
                    {
                        htmlPanel.splitCon.Panel2Collapsed = true;

                        htmlPanel.MainToolStrip.CodeToolStripButton.Checked = false;
                        htmlPanel.MainToolStrip.SplitToolStripButton.Checked = false;

                        /*if (!htmlPanel.DesignWebBrowser.Focused)
                        {
                            htmlPanel.CodeToDesign();
                        }*/
                        break;
                    }
                case DesignerOpenType.Code:
                    {
                        htmlPanel.splitCon.Panel1Collapsed = true;

                        htmlPanel.MainToolStrip.DesignToolStripButton.Checked = false;
                        htmlPanel.MainToolStrip.SplitToolStripButton.Checked = false;
                        /*if (htmlPanel.DesignWebBrowser.Focused)
                        {
                            htmlPanel.DesignToCode();
                        }*/
                        break;
                    }
                case DesignerOpenType.Spliter:
                    {
                        htmlPanel.splitCon.Panel1Collapsed = false;
                        htmlPanel.splitCon.Panel2Collapsed = false;

                        htmlPanel.MainToolStrip.DesignToolStripButton.Checked = false;
                        htmlPanel.MainToolStrip.CodeToolStripButton.Checked = false;
                        if (designPanelHeight>0)
                        htmlPanel.splitCon.SplitterDistance = designPanelHeight;

                       /* if (htmlPanel.DesignWebBrowser.Focused)
                        {
                            htmlPanel.DesignToCode();
                        }
                        else
                        {
                            htmlPanel.CodeToDesign();
                        }*/
                        break;
                    }
            }
        }

        public static Color GetColor(String color)
        {
            return Color.FromArgb(
                Convert.ToInt32(color.Substring(0, 2), 16),
                Convert.ToInt32(color.Substring(2, 2), 16),
                Convert.ToInt32(color.Substring(4, 2), 16));
        }

        public static void drawLine(Control con, Color clr, int width, DashStyle ds,Point bp, Point ep)
        {
            Graphics myg =con.CreateGraphics();
            Pen pen = new Pen(clr, width);
            pen.DashStyle = ds;
            myg.DrawLine(pen,bp,ep);
        }

        public static void drawLineForProPanel(Control con, Color clr, int width, DashStyle ds,int widstr)
        {
            Graphics myg = con.CreateGraphics();
            Pen pen = new Pen(clr, width);
            pen.DashStyle = ds;
            Point bp=new Point(0,54);
            Point ep = new Point(widstr,54);
            myg.DrawLine(pen, bp, ep);
        }
    }
}
