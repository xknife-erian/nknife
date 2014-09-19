using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
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
            path = PathService.Config_FontList;
            _fontDoc = new XmlDocument();
            if (File.Exists(path))
            {
                try 
                {
                    _fontDoc.Load(path);
                }
                catch
                {
                    File.Delete (path );
                    throw;
                }
                
            }
            else
            {
                _fontDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?><Font></Font>");
            }
        }

        static public void AddFontList(string[] fontItems,string tip)
        {
            XmlElement ele = FontDoc.DocumentElement;
            if (ele.HasChildNodes)
            {
                ele.RemoveAll();
            }

            List <string> lt = new List<string> ();

            for (int i = 0; i < fontItems.Length ; i++)
            {
                if (lt.Count != 0)
                {
                    if (!lt.Contains (fontItems[i]))
                    {
                        lt.Add (fontItems [i]);
                    }
                }
                else
                {
                    lt.Add(fontItems[i]);
                }
            }

            foreach (string item in lt)
            {
                if (String.Compare(item, tip) != 0)
                {
                    XmlElement styleEle = FontDoc.CreateElement("style");
                    styleEle.InnerText = item;
                    ele.AppendChild(styleEle);
                }
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
