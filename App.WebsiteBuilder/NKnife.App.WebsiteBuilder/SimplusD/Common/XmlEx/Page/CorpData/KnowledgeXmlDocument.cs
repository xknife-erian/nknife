using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD
{
    [PageCustom(true)]
    public class KnowledgeXmlDocument : CorpDataXmlDocument,ISearch
    {
        public KnowledgeXmlDocument(string relativeFilePath,SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }

        #region  元素的属性
        /// <summary>
        /// 知识名称(TextBox)
        /// </summary> 
        [Editor(1, 0, "KnowledgeName", MainControlWidth = 180, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "baseProperty", LabelRight = "BaseLabel2",IsRed=true,TextMaxLength=20,IsCanFind=true)]
        [SnipPart("KnowledgeName", "KnowledgeName", "KnowledgeName", "KnowledgeName", 0, 80)]
        [PropertyPad(0, 0, "KnowledgeName", MainControlWidth = 120, GroupBoxMainImage = @"Image\knowledge.png", LabelRight = "BaseLabel2", GroupBoxDockTop = false, IsRed = true,TextMaxLength=20)]
        public string KnowledgeName
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("productName")); }
            set { this.DocumentElement.SetAttribute("productName", value); }
        }
        /// <summary>
        ///链接地址(TextBox)
        /// </summary>
        [SnipPart("partUrl", "partUrl", "partUrl", "partUrlByname", 0, 80)]
        [Editor(1, 2, "Plink", MainControlWidth = 330,IsCanFind=true)]
        [PropertyPad(0, 1, "Plink", MainControlWidth =180)]
        public string Link
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("link")); }
            set { this.DocumentElement.SetAttribute("link", value); }

        }
        /// <summary>
        /// 知识类型(ComboBox)
        /// </summary>
        /// 

        [Editor(1, 1, "Knowledgetype", MainControlType = MainControlType.ComboBoxGroupControl,
            MainControlBindingFile = "KnowType.xml")]
        [SnipPart("Knowledgetype", "Knowledgetype", "Knowledgetype", "Knowledgetype", 0, 80)]
        public string [] MainSort
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//knowledgeSort");
                if (node == null)
                    return new string[0];
                List<string> values = new List<string>();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement ele = (XmlElement)subNode;
                            values.Add(ele.GetAttribute("value"));
                        }
                    }
                }
                return values.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//knowledgeSort");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("knowledgeSort");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.CreateElement("item");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }
        /// <summary>
        ///知识介绍
        /// </summary>
        [SnipPart("Description", "Description", "Description", "Description", 0, 80)]
        [Editor(8, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "knowledgeDes", LabelFooter = "BaseLabel4", IsRed = true,TextMaxLength=5000)]
        public string Description
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//description");
                if (node == null)
                    return string.Empty;
                else
                    return node.InnerText;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//description");
                if (node == null)
                {
                    XmlElement newEle = this.CreateElement("description");
                    this.DocumentElement.AppendChild(newEle);
                    node = (XmlNode)newEle;
                }
                node.RemoveAll();
                XmlCDataSection data = this.CreateCDataSection(value);
                node.AppendChild(data);
            }
        }
        #endregion


        #region ISearch 成员

        Position ISearch.SearchNext(WantToDoType type)
        {
            Position position = null;
            GetDataResourceValue getDataResourceValue = new GetDataResourceValue(this);
            switch (type)
            {
                case WantToDoType.SearchNext:
                    position = getDataResourceValue.SaveFindProperty(type);
                    break;
                case WantToDoType.SearchAll:
                    position = getDataResourceValue.SearchOrReplaceAll(type);
                    break;
                case WantToDoType.ReplaceNext:
                    position = getDataResourceValue.SaveFindProperty(type);
                    break;
                case WantToDoType.ReplaceAll:
                    position = getDataResourceValue.SearchOrReplaceAll(type);
                    break;
            }
            return position;
        }
        void ISearch.Replace(Position position)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
