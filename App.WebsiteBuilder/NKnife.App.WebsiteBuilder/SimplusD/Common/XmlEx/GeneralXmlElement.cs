using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��ͨ��XmlElement����ʾ��IndexXmleElement�ġ�
    /// </summary>
    public class GeneralXmlElement : AnyXmlElement
    {
        internal GeneralXmlElement(string localName, XmlDocument doc)
            :base(localName,doc)
        {
        }
    }
}
