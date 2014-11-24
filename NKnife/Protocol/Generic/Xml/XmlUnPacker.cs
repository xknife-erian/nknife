﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Common.Logging;
using NKnife.Interface;
using NKnife.Utility;

namespace NKnife.Protocol.Generic.Xml
{
    public class XmlProtocolUnPacker : StringProtocolUnPacker
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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
                    var ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XmlProtocolNames.Infos);
                    if (ele != null)
                        ParseInfos(content, ele);
                    ele = (XmlElement) doc.DocumentElement.SelectSingleNode(XmlProtocolNames.Tags);
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
            foreach (XmlNode node in infoElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    var ele = node as XmlElement;
                    if (ele != null)
                        content.Infomations.Add(ele.LocalName, ele.InnerText);
                }
            }
        }

        protected virtual void ParseParm(StringProtocolContent content, XmlElement docElement)
        {
            if (docElement.HasAttribute(XmlProtocolNames.Param))
            {
                content.CommandParam = docElement.GetAttribute(XmlProtocolNames.Param);
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
                    const BindingFlags BF = BindingFlags.CreateInstance |
                                            (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance));
                    obj = Activator.CreateInstance(type, BF, null, null, null);
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