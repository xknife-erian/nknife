using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu
{
    /// <summary>
    /// Xhtml1.0标签
    /// </summary>
    public sealed class XhtmlTagNameTabel : NameTable
    {
        private XhtmlTagNameTabel()
        {
            this.InitializeComponent();
        }

        static XhtmlTagNameTabel tabel;
        public static XhtmlTagNameTabel SingularCreator()
        {
            if (tabel == null)
            {
                tabel = new XhtmlTagNameTabel();
            }
            return tabel;
        }

        private void InitializeComponent()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("");//TODO:XhtmlRes.tagname);
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                if (item.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                this.Add(item.InnerText);
            }
        }

    }
}
