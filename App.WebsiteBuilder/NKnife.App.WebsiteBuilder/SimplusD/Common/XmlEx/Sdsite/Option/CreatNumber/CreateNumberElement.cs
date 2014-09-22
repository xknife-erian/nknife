using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Jeelu.SimplusD
{
    public class CreateNumberElement : GeneralXmlElement
    {
        protected List<int> list;
        public CreateNumberElement(XmlDocument doc)
            : base("createNumber", doc)
        {
        }

        public new DesignDataXmlDocument OwnerDocument
        {
            get
            {
                return (DesignDataXmlDocument)base.OwnerDocument;
            }
        }

        /// <summary>
        /// 自动生成的编码
        /// </summary>
        public string Number { get; set; }
        public string PageName { get; set; }
        public string OldNumber { get; set; }
        /// <summary>
        /// 添加一个节点
        /// </summary>
        public void AddElement()
        {
            CreateList();
            if (list != null)
            {
                if (list.Contains(Utility.Convert.PxStringToInt(Number.Substring(4))))
                {
                    MessageService.Show("该编号以存在");
                    return;
                }
            }
            XmlNode createrNumberNode = OwnerDocument.SelectSingleNode("//createNumber");
            if (createrNumberNode == null)
            {
                XmlElement newCreateNumberEle = OwnerDocument.CreateElement("createNumber");
                OwnerDocument.DocumentElement.AppendChild(newCreateNumberEle);
                createrNumberNode = (XmlNode)newCreateNumberEle;
            }
            string eleNode = PageName + "CreateNumber";
            XmlNode node = OwnerDocument.SelectSingleNode("//" + eleNode + "");
            if (node == null)
            {
                XmlElement newEle = OwnerDocument.CreateElement(PageName + "CreateNumber");
                createrNumberNode.AppendChild(newEle);
                node = (XmlNode)newEle;
            }
            XmlElement ele = OwnerDocument.CreateElement("numberItem");
            ele.SetAttribute("number", Number);
            node.AppendChild(ele);
            OwnerDocument.Save();
        }
        public void DelElement()
        {
            XmlNode node = OwnerDocument.SelectSingleNode("//createNumber");
            if (node == null)
                return;
            foreach (XmlElement ele in node.ChildNodes)
            {
                if (ele.Name.Equals(PageName + "CreateNumber"))
                {
                    foreach (XmlElement eleChild in ele.ChildNodes)
                    {
                        if (eleChild.GetAttribute("number").Equals(OldNumber))
                        {
                            ele.RemoveChild(eleChild);
                        }
                    }
                }
            }
        }
        public void CreateList()
        {

            string eleNode = PageName + "CreateNumber";
            XmlNode node = OwnerDocument.SelectSingleNode("//" + eleNode + "");
            if (node != null && node is XmlElement)
            {
                list = new List<int>();
                foreach (XmlElement ele in node.ChildNodes)
                {
                    if (!string.IsNullOrEmpty(ele.GetAttribute("number")))
                    {
                        int index = Utility.Convert.PxStringToInt(ele.GetAttribute("number").Substring(4));
                        list.Add(index);
                    }
                }
            }
        }
        public void CreateNumber()
        {
            CreateList();
            if (list != null)
            {
                list.Sort();
                int max = list[list.Count - 1];
                max++;
                Number = PageName + "00" + max;
            }
            else
                Number = PageName + "001";
        }
    }
}