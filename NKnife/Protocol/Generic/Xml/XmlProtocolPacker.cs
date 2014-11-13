﻿using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using NKnife.Interface;
using NKnife.Utility;

namespace NKnife.Protocol.Generic.Xml
{
    /// <summary>
    /// 描述一个将协议内容按指定的格式组装成一个指定类型(一般是字符串，但也可以是任何，如文件)
    /// </summary>
    public class XmlProtocolPacker : StringProtocolPacker
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region IProtocolPackage Members

        /// <summary>
        /// Combines the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public override string Combine(StringProtocolContent content)
        {
            using (var stream = new MemoryStream())
            {
                using (XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    try
                    {
                        WriteRoot(content, writer);
                        WriteDatas(content, writer);
                        WriteTags(content, writer);
                        WriteInformation(content, writer);
                    }
                    catch (Exception e)
                    {
                        _logger.Warn("协议生成字符流时异常", e);
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    var data = new byte[stream.Length];
                    Array.Copy(stream.GetBuffer(), data, stream.Length);
                    return Encoding.UTF8.GetString(data);
                }
            }
        }

        #endregion

        protected virtual void WriteInformation(StringProtocolContent content, XmlWriter writer)
        {
            // Infomations:固定数据，按协议规定的必须每次携带的数据。
            if (content.Infomations.Count > 0)
            {
                writer.WriteStartElement("Infos");
                foreach (var item in content.Infomations)
                {
                    writer.WriteAttributeString(item.Key, item.Value);
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteTags(StringProtocolContent content, XmlWriter writer)
        {
            // Datas,Tags,Infomations均属于协议的内容。
            // Tags:内容较大的数据，如序列化的对象等。
            if (null != content.Tags && content.Tags.Count > 0)
            {
                writer.WriteStartElement("Tags");
                foreach (object tag in content.Tags)
                {
                    if (tag is XmlElement)
                    {
                        ((XmlElement) tag).WriteTo(writer);
                    }
                    else if (tag is DataTable)
                    {
                        writer.WriteAttributeString("type", typeof (DataTable).FullName);
                        var dt = (DataTable) tag;
                        dt.WriteXml(writer, XmlWriteMode.WriteSchema);
                    }
                    else if (tag is string[])
                    {
                        var ser = new XmlSerializer(typeof (string[]));
                        ser.Serialize(writer, tag);
                    }
                    else if (tag is ISerializable)
                    {
                        string serializeString = UtilitySerialize.Serialize(tag);
                        writer.WriteAttributeString("type", tag.GetType().FullName);
                        writer.WriteCData(serializeString);
                    }
                    else
                    {
                        writer.WriteAttributeString("type", tag.GetType().FullName);
                        writer.WriteCData(tag.ToString());
                    }
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteDatas(StringProtocolContent content, XmlWriter writer)
        {
            // Datas,Tags,Infomations均属于协议的内容。
            // Datas:一般的简单数据，（一般较短）。
            if (content.Infomations.Count > 0)
            {
                writer.WriteStartElement("Datas");
                foreach (var infomation in content.Infomations)
                {
                    writer.WriteAttributeString(infomation.Key, infomation.Value);
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteRoot(StringProtocolContent content, XmlWriter writer)
        {
            writer.WriteStartElement(content.Command);
            if (content.CommandParam != null)
            {
                //命令参数
                if (!string.IsNullOrEmpty(content.CommandParam))
                    writer.WriteAttributeString("Param", content.CommandParam);
            }
        }
    }
}