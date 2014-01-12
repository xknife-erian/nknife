using System;
using System.Xml;
using Gean;

namespace NKnife.Interface.Models
{
    [Serializable]
    public abstract class AbstractModel : IModel, IToXmlElement
    {
        public abstract XmlElement ToXml(XmlDocument doc);
        public abstract void Parse(XmlElement element);

        protected static XmlElement BuildElement(XmlDocument doc, string localname, object value)
        {
            XmlElement ele = doc.CreateElement(localname);
            if (value != null)
                ele.InnerText = value.ToString();
            return ele;
        }
    }
}
