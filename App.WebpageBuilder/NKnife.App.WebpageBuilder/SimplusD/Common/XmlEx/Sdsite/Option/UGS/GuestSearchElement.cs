using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    public class GuestSearchElement : GeneralXmlElement
    {
        internal GuestSearchElement(XmlDocument doc)
            : base("guestSearch", doc)
        {
        }

        //建立节点名称
        public string CreatNodeName()
        {
            string nodeName = string.Empty;
            nodeName = "childSearchContent";
            return nodeName;
        }
        //创建频道的相关节点
        public XmlElement CreateNode(string SearchContent)
        {
            string nodeName = string.Empty;
            XmlElement nodeElement = null;
            nodeName = CreatNodeName();
            AnyXmlElement singleElement = this.GetElementByName(nodeName);
            if (singleElement == null)
            {
                nodeElement = this.OwnerDocument.CreateElement(nodeName);
                this.AppendChild(nodeElement);
            }
            else
            {
                nodeElement = singleElement;
            }
            CreadChildNode(nodeElement, SearchContent);
            this.OwnerAnyDocument.Save();
            return nodeElement;
        }
        //创建Search内容的相关节点
        public void CreadChildNode(XmlElement nodeElement, string SearchContent)
        {
            bool tagIsEquals = false;
            SearchContentElement childElement = this.OwnerDocument.CreateElement("searchContent") as SearchContentElement;
            if (nodeElement.ChildNodes.Count == 0)
            {
                childElement.SearchContent = SearchContent;
                nodeElement.AppendChild(childElement);
            }
            else
            {
                foreach (SearchContentElement childNode in nodeElement.ChildNodes)
                {
                    if (SearchContent.Equals(childNode.SearchContent))
                    {
                        childElement.SearchContent = SearchContent;
                        nodeElement.RemoveChild(childNode);
                        nodeElement.InsertBefore(childElement, nodeElement.FirstChild);
                        break;
                    }
                    else
                    {
                        tagIsEquals = true;
                    }
                }
                if (tagIsEquals && nodeElement.ChildNodes.Count == 10)
                {
                    childElement.SearchContent = SearchContent;
                    nodeElement.InsertBefore(childElement, nodeElement.FirstChild);
                    XmlNode lastNode = nodeElement.LastChild;
                    nodeElement.RemoveChild(lastNode);
                }
                if (tagIsEquals && nodeElement.ChildNodes.Count < 10)
                {
                    childElement.SearchContent = SearchContent;
                    nodeElement.InsertBefore(childElement, nodeElement.FirstChild);
                }
            }
        }
    }
}