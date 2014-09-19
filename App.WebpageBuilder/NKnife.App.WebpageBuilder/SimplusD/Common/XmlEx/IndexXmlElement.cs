using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��Id��Title��XmlElement��
    /// </summary>
    public abstract class IndexXmlElement : AnyXmlElement
    {
        protected IndexXmlElement(string localName, XmlDocument doc)
            :base(localName,doc)
        {
        }

        /// <summary>
        /// ��ȡ�����ýڵ�id��SimplusD���������һ��ΪGuid
        /// </summary>
        public virtual string Id
        {
            get { return this.GetAttribute("id"); }
            set { this.SetAttribute("id", value); }
        }

        /// <summary>
        /// ��ȡ�����ýڵ��CustomId��SimplusD��������������û����ģ��ڵ�ǰ�ĵ�����Ψһ
        /// </summary>
        public virtual string CustomId
        {
            get { return this.GetAttribute("customId"); }
            set { this.SetAttribute("customId", value); }
        }

        /// <summary>
        /// ��ȡ�����ýڵ�Title
        /// </summary>
        public virtual string Title
        {
            get { return this.GetAttribute("title"); }
            set { this.SetAttribute("title", value); }
        }

        /// <summary>
        /// ��ȡ�����ýڵ��ļ���
        /// </summary>
        public virtual string FileName
        {
            get { return this.GetAttribute("fileName"); }
            set { this.SetAttribute("fileName", value); }
        }
    }
}
