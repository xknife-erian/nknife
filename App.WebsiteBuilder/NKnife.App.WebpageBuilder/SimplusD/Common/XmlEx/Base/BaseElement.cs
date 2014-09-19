using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class BaseElement<M> where M : BaseDocument
    {
        internal protected XmlElement _innerXmlEle;
        public M OwnerDocument { get; private set; }
        protected BaseElement(XmlElement xmlEle,M ownerDocument)
        {
            this._innerXmlEle = xmlEle;
            this.OwnerDocument = ownerDocument;
        }
    }
}
