using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Gean;
using Gean.Interface;
using NKnife.Extensions;
using NKnife.NetWork.Interfaces;
using NLog;

namespace NKnife.NetWork.Protocol.Implementation
{
    public class ProtocolXmlParser : IProtocolParser
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

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

        /// <summary>获取协议的版本号
        /// </summary>
        /// <value>The version.</value>
        public virtual short Version
        {
            get { return 1; }
        }

        /// <summary>开始执行协议的解析
        /// </summary>
        public virtual void Execute(ref IProtocolContent content, string data, string family, string command)
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
                _Logger.WarnE("非XML协议数据:" + data, e);
            }
            try
            {
                ParseVersion(doc.DocumentElement);
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
                _Logger.WarnE("解析协议数据异常。", e);
            }
        }

        #endregion

        protected virtual void ParseVersion(XmlElement element)
        {
            string v = element.GetAttribute(VersionLocalName);
            int version = -1;
            if (!int.TryParse(v, out version))
            {
                return;
            }
            if (version < Version)
            {
                _Logger.Info(string.Format("协议版本有差。解析器版本：{1}，协议数据版本：{0}。", version, Version));
            }
            else if (version > Version)
            {
                _Logger.Warn(string.Format("协议版本有差。解析器版本：{1}，协议数据版本：{0}。", version, Version));
            }
        }

        protected virtual void ParseInfos(IProtocolContent content, XmlElement infoElement)
        {
            foreach (XmlAttribute attr in infoElement.Attributes)
            {
                content.Infomations.Add(attr.LocalName, attr.Value);
            }
        }

        protected virtual void ParseDatas(IProtocolContent content, XmlElement dataElement)
        {
            foreach (XmlAttribute attr in dataElement.Attributes)
            {
                content.Datas.Add(attr.LocalName, attr.Value);
            }
        }

        protected virtual void ParseParm(IProtocolContent content, XmlElement docElement)
        {
            if (docElement.HasAttribute("Parm"))
            {
                content.CommandParam = docElement.GetAttribute("Parm");
            }
        }

        protected virtual void ParseTags(IProtocolContent content, XmlElement tagsElement)
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
                    const BindingFlags bf = 
                        BindingFlags.CreateInstance |
                        (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance));
                    obj = Activator.CreateInstance(type, bf, null, null, null);
                    if (obj is IToXmlElement)
                    {
                        ((IToXmlElement) obj).Parse(itemElement);
                    }
                }
                catch (Exception)
                {
                }
                content.Tags.Add(obj);
            }
        }
    }
}