using System;
using System.Diagnostics;
using System.Xml;
using Gean;
using NKnife.NetWork.Interfaces;
using NKnife.Utility;
using NLog;

namespace NKnife.NetWork.Protocol
{
    /// <summary>协议工具接口集合
    /// </summary>
    public class ProtocolTools : ICloneable
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        internal ProtocolTools()
        {
        }

        public IProtocolHead Head { get; private set; }
        public IProtocolTail Tail { get; private set; }
        public IProtocolPackage Package { get; private set; }
        public IProtocolParser Parser { get; private set; }

        #region ICloneable Members

        /// <summary>创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>作为此实例副本的新对象。</returns>
        public object Clone()
        {
            var t = new ProtocolTools {Head = Head, Tail = Tail, Package = Package, Parser = Parser};
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
                Package = (IProtocolPackage) obj;

            obj = Get(element, "IProtocolParser");
            if (obj != null)
                Parser = (IProtocolParser) obj;
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
                _Logger.Warn(string.Format("Protocol工具接口解析异常。{0},{1}", klass, e.Message), e);
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
        public ProtocolTools CloneTools()
        {
            return (ProtocolTools) Clone();
        }
    }
}