﻿using System;
using System.Diagnostics;
using System.Xml;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Generic
{
    /// <summary>协议工具接口集合
    /// </summary>
    public class DefaultProtocolTools : IProtocolTools, ICloneable
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        public IDatagramCommandParser CommandParser { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
        public IProtocolHead Head { get; private set; }
        public IProtocolTail Tail { get; private set; }
        public IProtocolPackager Packager { get; private set; }
        public IProtocolUnPackager UnPackager { get; private set; }
        [Inject]
        public IProtocolFamily Family { get; private set; }

        #region ICloneable Members

        /// <summary>创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>作为此实例副本的新对象。</returns>
        public object Clone()
        {
            var t = new DefaultProtocolTools {Head = Head, Tail = Tail, Packager = Packager, UnPackager = UnPackager};
            return t;
        }

        #endregion

        /// <summary>解析协议工具接口集合节点
        /// </summary>
        /// <param name="node">The node.</param>
        internal void Parse(XmlNode node)
        {
            if (null == node || node.NodeType != XmlNodeType.Element)
                return;
            var element = (XmlElement) node;

            object obj = Get(element, "IProtocolHead");
            if (obj != null)
                Head = (IProtocolHead) obj;

            obj = Get(element, "IProtocolTail");
            if (obj != null)
                Tail = (IProtocolTail) obj;

            obj = Get(element, "IProtocolPackage");
            if (obj != null)
                Packager = (IProtocolPackager) obj;

            obj = Get(element, "IProtocolParser");
            if (obj != null)
                UnPackager = (IProtocolUnPackager) obj;
        }

        private static Type GetContentType(XmlElement parentElement, string name)
        {
            Type type = null;
            string klass = string.Empty;
            try
            {
                string ns = parentElement.GetAttribute("namespace") + ".";
                XmlNode node = parentElement.SelectSingleNode(name);
                if (node == null)
                {
                    return null;
                }
                klass = ns + node.InnerText.Trim();
                type = UtilityType.FindType(klass);
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("Protocol工具接口解析异常。{0},{1}", klass, e.Message), e);
            }
            return type;
        }

        /// <summary>通过配置文件为该协议家族的工具接口创建实例
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static object Get(XmlElement parentElement, string name)
        {
            Type type = GetContentType(parentElement, name);
            if (null != type)
            {
                try
                {
                    object obj = Activator.CreateInstance(type);
                    return obj;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        /// <summary>创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>作为此实例副本的新对象。</returns>
        public DefaultProtocolTools CloneTools()
        {
            return (DefaultProtocolTools) Clone();
        }
    }
}