﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NKnife.Utility;
using NLog;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Protocols;

namespace SocketKnife.Protocol.Implementation
{
    /// <summary>
    ///     含有序列化对象的解析器
    /// </summary>
    public class ProtocolDeserializeParser : ProtocolXmlParser
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected override void ParseTags(IProtocolContent content, XmlElement tagsElement)
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
                    _Logger.Warn(string.Format("反序列化协议Tag异常。{0}", ex.Message), ex);
                }
                content.Tags.Add(obj);
            }
        }
    }
}