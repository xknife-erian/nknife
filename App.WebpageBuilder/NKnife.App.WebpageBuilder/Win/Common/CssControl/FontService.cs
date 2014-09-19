using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace Jeelu.Win
{
    static internal class FontService
    {
        static string path;

        static private XmlDocument _fontDoc;
        static public XmlDocument FontDoc
        {
            get { return _fontDoc; }
            set { _fontDoc = value; }
        }
        /// <summary>
        /// 添加字体列表到文件
        /// </summary>
        static FontService()
        {
            path = Path.Combine(Application.StartupPath, "Config/fontlist.xml"); ;
            _fontDoc = new XmlDocument();
            if (File.Exists(path))
            {
                try
                {
                    _fontDoc.Load(path);
                }
                catch
                {
                    File.Delete(path);
                    throw;
                }

            }
            else
            {
                _fontDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?><Font></Font>");
            }
        }

        static public void AddFontList(List<string> fontItems)
        {
            if (fontItems.Count < 0) return;

            XmlElement ele = FontDoc.DocumentElement;
            if (ele.HasChildNodes)
            {
                ele.RemoveAll();
            }       

            foreach (string item in fontItems)
            {                
                XmlElement styleEle = FontDoc.CreateElement("style");
                styleEle.InnerText = item;
                ele.AppendChild(styleEle);                
            }
            _fontDoc.Save(path);
        }

        static public List<string> ReadFontList()
        {
            List<string> lt = new List<string>();
            XmlElement ele = FontDoc.DocumentElement;
            XmlNodeList xnl = ele.ChildNodes;
            if (xnl != null)
            {
                foreach (XmlNode node in xnl)
                {
                    lt.Add(node.InnerText);
                }
            }
            return lt;
        }
    }
}
