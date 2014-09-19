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
        /// ��ȡ�ڵ��Ƿ������XmlDocument��(��ɾ����ճ�ʼ����û��ӵ�XmlDocument��)
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
        /// ͨ���ӽڵ�Id���AnyXmlElement
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
        /// �ڵ��CDATA�ڵ�ֵ
        /// Ϊ��֤���ݵ��ݴ����ܣ�����һ�㶼���洢��CDATA�ڵ���
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
        /// ��ȡ�ýڵ�������AnyXmlDocument
        /// </summary>
        public AnyXmlDocument OwnerAnyDocument
        {
            get { return (AnyXmlDocument)this.OwnerDocument; }
        }

        #region ��չXPath�ķ���,XmlDocument��XmlElement�Ĵ���Ӧ������ȫһ����

        /// <summary> 
        /// ������,ͨ���ڵ��id���XmlElement
        /// </summary>
        public virtual AnyXmlElement GetElementById(string id)
        {
            return GetElementById(id, "*");
        }

        /// <summary> 
        /// ������,ͨ���ڵ��id��name���XmlElement
        /// </summary>
        public virtual AnyXmlElement GetElementById(string id, string name)
        {
            return (AnyXmlElement)XPathHelper.GetElementById(this, id, name);
        }

        /// <summary>
        /// ͨ���ڵ��name���XmlElement��
        /// </summary>
        public virtual AnyXmlElement GetElementByName(string name)
        {
            return (AnyXmlElement)XPathHelper.GetElementByName(this, name);
        }

        /// <summary>
        /// �ڵ�ǰ���ӽڵ��л��ָ���Ľڵ����Ľڵ㼯��
        /// </summary>
        public virtual XmlNodeList GetChildsByName(string name)
        {
            return XPathHelper.GetChildsByName(this, name);
        }
        /// <summary>
        /// �ڵ�ǰ���ӽڵ��л��ָ���Ľڵ����Ľڵ㼯��
        /// </summary>
        public virtual XmlNodeList GetChildsByName(string name,bool isGetDeleted)
        {
            return XPathHelper.GetChildsByName(this, name,isGetDeleted);
        }


        #endregion
    }
}