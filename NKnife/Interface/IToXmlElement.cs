using System.Xml;

namespace NKnife.Interface
{
    public interface IToXmlElement
    {
        XmlElement ToXml(XmlDocument doc);

        void Parse(XmlElement element);
    }
}
