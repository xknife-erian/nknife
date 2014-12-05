﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using NKnife.Utility;

namespace NKnife.Protocol.Generic.Xml
{
    /// <summary>
    /// 描述一个将协议内容按指定的格式组装成一个指定类型(一般是字符串，但也可以是任何，如文件)
    /// </summary>
    public class XmlProtocolPacker : StringProtocolPacker
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 是否快速封包,当快速封包时,Tags中的对象默认为String对象,而不考虑序列化
        /// </summary>
        public bool EnableQuickCombine { get; set; }

        public XmlProtocolPacker()
        {
            EnableQuickCombine = true;
        }

        #region IProtocolPackage Members

        /// <summary>
        /// Combines the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public override string Combine(StringProtocol content)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(stream, Encoding.Default))
                {
                    try
                    {
                        WriteRoot(content, writer);
                        WriteTags(content, writer);
                        WriteInformation(content, writer);
                    }
                    catch (Exception e)
                    {
                        _logger.Warn("协议生成字符流时异常", e);
                    }
                    writer.WriteEndElement();//关闭ROOT节点
                    writer.Flush();
                    var data = new byte[stream.Length];
                    Array.Copy(stream.GetBuffer(), data, stream.Length);
                    var c = Encoding.Default.GetString(data);
                    return c;
                }
            }
        }

        #endregion

        protected virtual void WriteInformation(StringProtocol content, XmlWriter writer)
        {
            if (content.Infomations.Count > 0)
            {
                writer.WriteStartElement(XmlProtocolNames.Infos);
                foreach (var item in content.Infomations)
                {
                    writer.WriteElementString(item.Key, item.Value);
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteTags(StringProtocol content, XmlWriter writer)
        {
            if (null != content.Tags && content.Tags.Count > 0)
            {
                writer.WriteStartElement(XmlProtocolNames.Tags);
                foreach (object tag in content.Tags)
                {
                    if (tag == null)
                        continue;
                    if (EnableQuickCombine || tag is string)
                    {
                        writer.WriteElementString(XmlProtocolNames.Tag, tag.ToString());
                    }
                    else if (tag is IEnumerable<object>)
                    {
                        var ser = new XmlSerializer(typeof(IEnumerable<object>));
                        ser.Serialize(writer, tag);
                    }
                    else if (tag is XmlElement)
                    {
                        ((XmlElement) tag).WriteTo(writer);
                    }
                    else if (tag is DataTable)
                    {
                        writer.WriteAttributeString(XmlProtocolNames.Type, typeof(DataTable).FullName);
                        var dt = (DataTable) tag;
                        dt.WriteXml(writer, XmlWriteMode.WriteSchema);
                    }
                    else if (tag is ISerializable)
                    {
                        string serializeString = UtilitySerialize.Serialize(tag);
                        writer.WriteAttributeString(XmlProtocolNames.Type, tag.GetType().FullName);
                        writer.WriteCData(serializeString);
                    }
                    else
                    {
                        writer.WriteAttributeString(XmlProtocolNames.Type, tag.GetType().FullName);
                        writer.WriteCData(tag.ToString());
                    }
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteRoot(StringProtocol content, XmlWriter writer)
        {
            writer.WriteStartElement(content.Command);
            if (content.CommandParam != null)
            {
                //命令参数
                if (!string.IsNullOrEmpty(content.CommandParam))
                    writer.WriteAttributeString(XmlProtocolNames.Param, content.CommandParam);
            }
        }
    }
}