using System.Xml;

namespace NKnife.Interface
{
    public interface IXml
    {
        XmlElement ToXml(XmlElement parent);

        void Parse(XmlElement element);
    }
}
