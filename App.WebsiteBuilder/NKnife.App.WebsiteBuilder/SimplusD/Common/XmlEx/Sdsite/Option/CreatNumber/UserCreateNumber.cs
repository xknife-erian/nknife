using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Jeelu.SimplusD
{
    public class UserCreateNumber : GeneralXmlElement
    {
        private List<string> list;
        public UserCreateNumber(XmlDocument doc)
            : base("userCreateNumber", doc)
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
        /// 用户自定义的编号
        /// </summary>
        public string PageName { get; set; }
        public string UserAddNumber { get; set; }
        public string UserOldNumber { get; set; }
        public void CreateList()
        {
            string eleNode = PageName + "UserCreateNumber";
            XmlNode node = OwnerDocument.SelectSingleNode("//" + eleNode + "");
            if (node != null && node is XmlElement)
            {
                list = new List<string>();
                foreach (XmlElement ele in node.ChildNodes)
                {
                    if (!string.IsNullOrEmpty(ele.GetAttribute("number")))
                    {
                        string index = ele.GetAttribute("number");
                        list.Add(index);
                    }
                }
            }

        }
        public void CreateElement()
        {
            CreateList();
            if(list!=null)
            {
                if (list.Contains(UserAddNumber))
                {
                    MessageService.Show("该编号以存在");
                    return;
                }
            }
            XmlNode createrNumberNode = OwnerDocument.SelectSingleNode("//userAddNumber");
            if (createrNumberNode == null)
            {
                XmlElement newCreateNumberEle = OwnerDocument.CreateElement("userAddNumber");
                OwnerDocument.DocumentElement.AppendChild(newCreateNumberEle);
                createrNumberNode = (XmlNode)newCreateNumberEle;
            }
            string eleNode = PageName + "UserCreateNumber";
            XmlNode node = OwnerDocument.SelectSingleNode("//" + eleNode + "");
            if (node == null)
            {
                XmlElement newEle = OwnerDocument.CreateElement(PageName + "UserCreateNumber");
                createrNumberNode.AppendChild(newEle);
                node = (XmlNode)newEle;
            }
            XmlElement ele = OwnerDocument.CreateElement("numberItem");
            ele.SetAttribute("number", UserAddNumber);
            node.AppendChild(ele);
            OwnerDocument.Save();
        }
        public void DeleElement()
        {
            XmlNode node = OwnerDocument.SelectSingleNode("//userAddNumber");
            if (node == null)
                return;
            foreach (XmlElement ele in node.ChildNodes)
            {
                if (ele.Name.Equals(PageName + "UserCreateNumber"))
                {
                    foreach (XmlElement eleChild in ele.ChildNodes)
                    {
                        if (eleChild.GetAttribute("number").Equals(UserOldNumber))
                        {
                            ele.RemoveChild(eleChild);
                        }
                    }
                }
            }
        }
    }
}
