using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class RefFileElement : BaseElement<SdrefsDocument>
    {
        internal RefFileElement(XmlElement ele,SdrefsDocument ownerDocument)
            : base(ele, ownerDocument)
        {
        }
        /// <summary>
        /// 客户ID（为客户做标识）
        /// </summary>
        public string CustomerId
        {
            get { return this._innerXmlEle.GetAttribute("customerId"); }
            set { this._innerXmlEle.SetAttribute("customerId",value.ToString()); }
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
            set { this._innerXmlEle.SetAttribute("type",value.ToString()); }
        }

    }
}
