using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.Win
{
    public class AutoLayoutPanelXmlDocument : XmlDocument
    {
        public FileInfo XmlFile { get; set; }

        private static AutoLayoutPanelXmlDocument _singDoc = null;
        internal static AutoLayoutPanelXmlDocument Singler
        {
            get
            {
                if (_singDoc == null)
                {
                    _singDoc = new AutoLayoutPanelXmlDocument();
                }
                return _singDoc;
            }
        }
        //获取资源文件的路径，得到相应的数据文件 by lisuye on 2008年5月29日
        private AutoLayoutPanelXmlDocument()
        {
            string path = Path.Combine(Application.StartupPath, @"CHS");//TODO:lukan , PathService
            string fileFullName = "AutoLayoutPanelResource.xml";

            this.XmlFile = new FileInfo(Path.Combine(path, fileFullName));
            this.Load(this.XmlFile.FullName);

            if (TextDic == null)
            {
                TextDic = this.FillTextDic();
            }
        }

        internal Dictionary<string, string> TextDic { get; set; }

        /// <summary>
        /// 从选项文件中将所有的语言文本填充入Dictionary
        /// </summary>
       //获取资源文件将资源文件保存到Dictionary by lisuye on 2008年5月29日
        private Dictionary<string, string> FillTextDic()
        {
            XmlElement textEle = (XmlElement)this.DocumentElement.SelectSingleNode("//texts");
            Debug.Assert(!(textEle == null), "AutoLayoutPanelResource.xml is Bad !!!");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlNode node in textEle.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                string key = ele.GetAttribute("name");
                string textValue = ele.InnerText;
                dic.Add(key, textValue);
            }
            return dic;
        }
    }
}
