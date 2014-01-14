using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Gean.Xml;
using NKnife.Extensions;
using NLog;

namespace Gean.Net.Protocol.General
{
    /// <summary>
    /// 描述一个将协议内容按指定的格式组装成一个指定类型(一般是字符串，但也可以是任何，如文件)
    /// </summary>
    public class GeneralProtocolXmlPackage : IProtocolPackage
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 当前IProtocolPackage实现类型的版本。
        /// </summary>
        /// <value>The version.</value>
        public short Version
        {
            get { return 1; }
        }

        /// <summary>
        /// Combines the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string Combine(IProtocolContent content)
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
                        _Logger.WarnE("协议生成字符流时异常", e);
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    var data = new byte[stream.Length];
                    Array.Copy(stream.GetBuffer(), data, stream.Length);
                    return Encoding.UTF8.GetString(data);
                }
            }
        }

        protected virtual void WriteInformation(IProtocolContent content, XmlWriter writer)
        {
            // Infomations:固定数据，按协议规定的必须每次携带的数据。
            if (content.Infomations.Count > 0)
            {
                writer.WriteStartElement("Infos");
                foreach (var item in content.Infomations)
                {
                    writer.WriteAttributeString(item.Key, item.Value.ToString());
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteTags(IProtocolContent content, XmlWriter writer)
        {
            // Datas,Tags,Infomations均属于协议的内容。
            // Tags:内容较大的数据，如序列化的对象等。
            if (null != content.Tags && content.Tags.Count > 0)
            {
                writer.WriteStartElement("Tags");
                foreach (var tag in content.Tags)
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
                        var ser = new XmlSerializer(typeof(string[]));
                        ser.Serialize(writer, tag);
                    } 
                    else if (tag is ISerializable)
                    {
                        var serializeString = UtilitySerialize.Serialize(tag);
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

        protected virtual void WriteDatas(IProtocolContent content, XmlWriter writer)
        {
            // Datas,Tags,Infomations均属于协议的内容。
            // Datas:一般的简单数据，（一般较短）。
            if (content.Datas.Count > 0)
            {
                writer.WriteStartElement("Datas");
                for (int i = 0; i < content.Datas.Count; i++)
                {
                    writer.WriteAttributeString(content.Datas.GetKey(i), content.Datas.Get(i));
                }
                writer.WriteEndElement();
            }
        }

        protected virtual void WriteRoot(IProtocolContent content, XmlWriter writer)
        {
            writer.WriteStartElement(content.Command);
            //协议版本号
            writer.WriteAttributeString("version", this.Version.ToString());
            if (content.CommandParam != null)
            {
                //命令参数
                if (!string.IsNullOrEmpty(content.CommandParam))
                    writer.WriteAttributeString("Parm", content.CommandParam.ToString());
            }
        }
    }
}