using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class AnyXmlElement : XmlElement
    {
        public AnyXmlElement(string localName,XmlDocument doc)
            :base("",localName,"",doc)
        {
        }

        public override XmlNode Clone()
        {
            AnyXmlElement targetEle = OwnerDocument.CreateElement(this.Name)
                as AnyXmlElement;
            XmlUtilService.CopyXmlElement(this, targetEle);
            return targetEle;
        }

        /// <summary>
        /// 获取节点是否存在于XmlDocument中(被删除或刚初始化还没添加到XmlDocument里)
        /// </summary>
        public bool IsInDocument
        {
            get
            {
                XmlNode tempParent = this.ParentNode;
                while (tempParent != null)
                {
                    if (tempParent.NodeType == XmlNodeType.Document)
                    {
                        return true;
                    }
                    tempParent = tempParent.ParentNode;
                }
                return false;
            }
        }

        /// <summary>
        /// 通过子节点Id获得AnyXmlElement
        /// </summary>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public new AnyXmlElement this[string childNodeName]
        {
            get
            {
                return (AnyXmlElement)base[childNodeName];
            }
        }

        public new AnyXmlElement this[string localName, string ns]
        {
            get
            {
                return (AnyXmlElement)base[localName, ns];
            }
        }

        /// <summary>
        /// 节点的CDATA节点值
        /// 为保证数据的容错性能，数据一般都将存储在CDATA节点下
        /// </summary>
        public string CDataValue
        {
            get
            {
                foreach (XmlNode node in this.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.CDATA)
                    {
                        return node.Value;
                    }
                }
                return "";
            }
            set
            {
                foreach (XmlNode node in this.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.CDATA)
                    {
                        node.Value = value;
                        return;
                    }
                }

                XmlCDataSection cdataText = this.OwnerDocument.CreateCDataSection(value);
                this.AppendChild(cdataText);
            }
        }

        /// <summary>
        /// 获取该节点所属的AnyXmlDocument
        /// </summary>
        public AnyXmlDocument OwnerAnyDocument
        {
            get { return (AnyXmlDocument)this.OwnerDocument; }
        }

        #region 扩展XPath的方法,XmlDocument和XmlElement的代码应该是完全一样的

        /// <summary> 
        /// 已重载,通过节点的id获得XmlElement
        /// </summary>
        public virtual AnyXmlElement GetElementById(string id)
        {
            return GetElementById(id, "*");
        }

        /// <summary> 
        /// 已重载,通过节点的id和name获得XmlElement
        /// </summary>
        public virtual AnyXmlElement GetElementById(string id, string name)
        {
            return (AnyXmlElement)XPathHelper.GetElementById(this, id, name);
        }

        /// <summary>
        /// 通过节点的name获得XmlElement。
        /// </summary>
        public virtual AnyXmlElement GetElementByName(string name)
        {
            return (AnyXmlElement)XPathHelper.GetElementByName(this, name);
        }

        /// <summary>
        /// 在当前的子节点中获得指定的节点名的节点集合
        /// </summary>
        public virtual XmlNodeList GetChildsByName(string name)
        {
            return XPathHelper.GetChildsByName(this, name);
        }
        /// <summary>
        /// 在当前的子节点中获得指定的节点名的节点集合
        /// </summary>
        public virtual XmlNodeList GetChildsByName(string name,bool isGetDeleted)
        {
            return XPathHelper.GetChildsByName(this, name,isGetDeleted);
        }


        #endregion
    }
}