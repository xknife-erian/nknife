using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;

namespace NKnife.Protocol.Generic.Packers
{
    /// <summary>含有序列化对象的解析器
    /// </summary>
    public class XmlProtocolDeserializeUnPacker : XmlProtocolUnPacker
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected override void ParseTags(StringProtocolContent content, XmlElement tagsElement)
        {
            content.Tags = new List<object>();
            foreach (XmlNode node in tagsElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.CDATA)
                    continue;
                var itemElement = (XmlCDataSection) node;
                object obj = null;
                Type type = UtilityType.FindType(tagsElement.GetAttribute("type"));
                try
                {
                    var xs = new XmlSerializer(type);
                    using (var stream = new MemoryStream(Encoding.Default.GetBytes(itemElement.InnerText)))
                    {
                        obj = xs.Deserialize(stream);
                        content.Tags.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("反序列化协议Tag异常。{0}", ex.Message), ex);
                }
                content.Tags.Add(obj);
            }
        }
    }
}