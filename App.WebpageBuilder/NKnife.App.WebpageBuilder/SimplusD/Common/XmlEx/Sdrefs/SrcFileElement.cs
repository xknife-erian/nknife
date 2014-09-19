using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class SrcFileElement : BaseElement<SdrefsDocument>
    {
        public ElementCollection<RefFileElement,SdrefsDocument> RefFiles { get; private set; }
        internal SrcFileElement(XmlElement ele, SdrefsDocument ownerDocument)
            :base(ele,ownerDocument)
        {
            RefFiles = new ElementCollection<RefFileElement, SdrefsDocument>(ownerDocument);
            RefFiles.Inserted += new EventHandler<EventArgs<RefFileElement>>(RefFiles_Inserted);
            RefFiles.Removed += new EventHandler<EventArgs<RefFileElement>>(RefFiles_Removed);
            RefFiles.ItemChanged += new EventHandler<ChangedEventArgs<RefFileElement>>(RefFiles_ItemChanged);
        }

        void RefFiles_ItemChanged(object sender, ChangedEventArgs<RefFileElement> e)
        {
            throw new NotImplementedException();
        }

        void RefFiles_Removed(object sender, EventArgs<RefFileElement> e)
        {
            this._innerXmlEle.AppendChild(e.Item._innerXmlEle);
        }

        void RefFiles_Inserted(object sender, EventArgs<RefFileElement> e)
        {
            this._innerXmlEle.RemoveChild(e.Item._innerXmlEle);
        }
        public RefFileElement GetSrcFileById(string id)
        {
            return this.RefFiles[id];
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string Id
        {
            get { return this._innerXmlEle.GetAttribute("id"); }
            internal set { this._innerXmlEle.SetAttribute("id", value.ToString()); }
        }
        /// <summary>
        /// 客户ID（为客户做标识）
        /// </summary>
        public string CustomId
        {
            get { return this._innerXmlEle.GetAttribute("customId"); }
            set { this._innerXmlEle.SetAttribute("customId", value.ToString()); }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public IdType Type
        {
            get
            {
                string strType = this._innerXmlEle.GetAttribute("type");
                return (IdType)Enum.Parse(typeof(IdType), strType);
            }
            internal set { this._innerXmlEle.SetAttribute("type", value.ToString()); }
        }

    }
}
