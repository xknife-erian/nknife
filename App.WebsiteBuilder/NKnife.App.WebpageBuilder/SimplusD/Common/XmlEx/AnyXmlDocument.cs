using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD
{
    public abstract class AnyXmlDocument : XmlDocument
    {
        public AnyXmlDocument(string absoluteFilePath)
            :base()
        {
            this.AbsoluteFilePath = absoluteFilePath;
        }

        /// <summary>
        /// 通过XmlElement的OuterXml返回的字符串生成XmlElement
        /// </summary>
        public XmlElement ParseElement(string outerXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(outerXml);

            XmlElement newEle = this.CreateElement(doc.DocumentElement.Name);

            XmlUtilService.CopyXmlElement(doc.DocumentElement, newEle);

            return newEle;
        }

        /// <summary>
        /// 当前XmlDocument文件的相对路径
        /// </summary>
        //public string RelativeFilePath { get; protected set; }

        /// <summary>
        /// 当前XmlDocument文件的绝对路径
        /// </summary>
        public string AbsoluteFilePath { get; protected set; }

        /// <summary>
        /// 加载当前XmlDocument
        /// </summary>
        virtual public void Load()
        {
            Load(AbsoluteFilePath);
        }

        /// <summary>
        /// 保存当前XmlDocument
        /// </summary>
        virtual public void Save()
        {
            FileAttributes fileAtts = FileAttributes.Normal;

            if (File.Exists(AbsoluteFilePath))
            {
                ///先获取此文件的属性
                fileAtts = File.GetAttributes(AbsoluteFilePath);

                ///讲文件属性设置为普通（即没有只读和隐藏等）
                File.SetAttributes(AbsoluteFilePath, FileAttributes.Normal);
            }

            ///在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            this.Save(AbsoluteFilePath);

            ///恢复文件属性
            File.SetAttributes(AbsoluteFilePath, fileAtts);
        }

        public void Save(bool isSaveSiteProperty)
        {
            Save();
            if (isSaveSiteProperty)
            {
                if (SitePropertySaved != null)
                {
                    SitePropertySaved(null, EventArgs.Empty);
                }
            }
        }


        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            return new AnyXmlElement(localName, this);
        }

        public event EventHandler SitePropertySaved;

        #region 扩展XPath的方法,XmlDocument和XmlElement的代码应该是完全一样的

        /// <summary> 
        /// 已重写,已重载,通过节点的id获得XmlElement
        /// 在.net的该方法采用了DTD，SimplusD.XmlEx中不会使用DTD，故重写该方法
        /// </summary>
        public new AnyXmlElement GetElementById(string id)
        {
            return GetElementById(id, "*");
        }

        /// <summary> 
        /// 已重写,已重载,通过节点的id和name获得XmlElement
        /// 在.net的该方法采用了DTD，SimplusD.XmlEx中不会使用DTD，故重写该方法
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

        #endregion

        #region IDisposable 成员

        public bool IsClosed { get; private set; }

        public void Close()
        {
            IsClosed = true;
            OnClosed(EventArgs.Empty);
        }

        public event EventHandler Closed;
        protected virtual void OnClosed(EventArgs e)
        {
            if (Closed != null)
            {
                Closed(this, e);
            }
        }

        #endregion
    }
}
