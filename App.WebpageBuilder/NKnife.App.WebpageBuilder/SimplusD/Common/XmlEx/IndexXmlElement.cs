using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 有Id和Title的XmlElement。
    /// </summary>
    public abstract class IndexXmlElement : AnyXmlElement
    {
        protected IndexXmlElement(string localName, XmlDocument doc)
            :base(localName,doc)
        {
        }

        /// <summary>
        /// 获取或设置节点id，SimplusD软件创建，一般为Guid
        /// </summary>
        public virtual string Id
        {
            get { return this.GetAttribute("id"); }
            set { this.SetAttribute("id", value); }
        }

        /// <summary>
        /// 获取或设置节点的CustomId，SimplusD软件创建，允许用户更改，在当前文档必须唯一
        /// </summary>
        public virtual string CustomId
        {
            get { return this.GetAttribute("customId"); }
            set { this.SetAttribute("customId", value); }
        }

        /// <summary>
        /// 获取或设置节点Title
        /// </summary>
        public virtual string Title
        {
            get { return this.GetAttribute("title"); }
            set { this.SetAttribute("title", value); }
        }

        /// <summary>
        /// 获取或设置节点文件名
        /// </summary>
        public virtual string FileName
        {
            get { return this.GetAttribute("fileName"); }
            set { this.SetAttribute("fileName", value); }
        }
    }
}
