using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Common.Logging;
using NKnife.Interface;
using NKnife.Protocol.Generic;
using NKnife.Utility;

namespace SocketKnife.Generic.Protocol.Xmls
{
    public class XmlProtocolUnPacker : StringProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public virtual string VersionLocalName
        {
            get { return "version"; }
        }

        public virtual string XPathDatas
        {
            get { return "Datas"; }
        }

        public virtual string XPathInfos
        {
            get { return "Infos"; }
        }

        public virtual string XPathTags
        {
            get { return "Tags"; }
        }

        #region IProtocolParser Members

        public override void Execute(StringProtocolContent content, string data, string family, string command)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(data);
            }
            catch (Exception e)
            {
                _logger.Warn("非XML协议数据:" + data, e);
            }
            try
            {
                ParseParm(content, doc.DocumentElement);
                if (doc.DocumentElement != null)
                {
                    var ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XPathDatas);
                    if (ele != null)
                        ParseDatas(content, ele);
                    ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XPathInfos);
                    if (ele != null)
                        ParseInfos(content, ele);
                    ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XPathTags);
                    if (ele != null)
                        ParseTags(content, ele);
                }
            }
            catch (Exception e)
            {
                _logger.Warn("解析协议数据异常。", e);
            }
        }

        #endregion

        protected virtual void ParseInfos(StringProtocolContent content, XmlElement infoElement)
        {
            foreach (XmlAttribute attr in infoElement.Attributes)
            {
                content.Infomations.Add(attr.LocalName, attr.Value);
            }
        }

        protected virtual void ParseDatas(StringProtocolContent content, XmlElement dataElement)
        {
            foreach (XmlAttribute attr in dataElement.Attributes)
            {
                content.Infomations.Add(attr.LocalName, attr.Value);
            }
        }

        protected virtual void ParseParm(StringProtocolContent content, XmlElement docElement)
        {
            if (docElement.HasAttribute("Param"))
            {
                content.CommandParam = docElement.GetAttribute("Param");
            }
        }

        protected virtual void ParseTags(StringProtocolContent content, XmlElement tagsElement)
        {
            content.Tags = new List<object>();
            foreach (XmlNode node in tagsElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;
                var itemElement = (XmlElement) node;
                object obj = null;
                Type type = UtilityType.FindType(itemElement.GetAttribute("class"));
                try
                {
                    const BindingFlags bf = BindingFlags.CreateInstance |
                        (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance));
                    obj = Activator.CreateInstance(type, bf, null, null, null);
                    var xml = obj as IXml;
                    if (xml != null)
                    {
                        xml.Parse(itemElement);
                    }
                }
                catch (Exception e)
                {
                    Debug.Fail("从Tag创建对象时异常", e.Message);
                }
                content.Tags.Add(obj);
            }
        }
    }
}