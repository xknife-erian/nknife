using System;
using System.Collections.Concurrent;
using System.Xml;
using Gean.Configuring.CoderSetting;
using Gean.Network.Protocol;
using NLog;

namespace Gean.Network.Config
{
    public abstract class ProtocolSetting : XmlCoderSetting
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected ProtocolSetting()
        {
            FamilyMap = new ConcurrentDictionary<string, ProtocolFamily>();
            ProtocolContentMap = new ConcurrentDictionary<string, Type>();
            ProtocolToolsMap = new ConcurrentDictionary<string, ProtocolTools>();
        }

        protected internal ConcurrentDictionary<string, ProtocolFamily> FamilyMap { get; set; }
        protected internal ConcurrentDictionary<string, Type> ProtocolContentMap { get; set; }
        protected internal ConcurrentDictionary<string, ProtocolTools> ProtocolToolsMap { get; set; }

        protected override void Load(XmlElement source)
        {
            _Logger.Trace("ProtocolSetting初始化");
            ParseProtocolFamily((XmlElement) source.SelectSingleNode("protocols"));
        }

        /// <summary>解析一个协议家族
        /// </summary>
        /// <param name="source">The source.</param>
        private void ParseProtocolFamily(XmlElement source)
        {
            try
            {
                foreach (XmlNode famNode in source.ChildNodes)
                {
                    if (famNode.NodeType != XmlNodeType.Element)
                        continue;
                    var famElement = (XmlElement) famNode;

                    //创建协议家族
                    string familyName = famElement.GetAttribute("name");
                    _Logger.Trace(string.Format("找到一个协议族:{0}", familyName));
                    string famNamespac = famElement.GetAttribute("namespace") + ".";
                    //家族默认的Content类型
                    var contentNode = famElement.SelectSingleNode("Content");
                    if (contentNode == null) 
                        continue;
                    var contentTxt = contentNode.InnerText.Trim();
                    var contentType = UtilityType.FindType(contentTxt);
                    var family = new ProtocolFamily(familyName, contentType);
                    //解析协议家族的默认工具接口
                    family.DefaultTools.Parse(famElement.SelectSingleNode("tools"));
                    //遍历节点，每节点均为一个协议
                    ParseProtocol(family, famElement.SelectSingleNode("impl"), famNamespac);
                    //解析完成，加入Map中
                    FamilyMap.TryAdd(familyName, family);
                }
                _Logger.Debug(string.Format("协议族工厂中共有{0}个族。", FamilyMap.Count));
            }
            catch (Exception e)
            {
                _Logger.ErrorE(string.Format("解析协议时异常。{0}", e.Message), e);
            }
        }

        /// <summary>根据协议Xml节点解析出协议
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="node">The node.</param>
        /// <param name="ns"></param>
        private void ParseProtocol(ProtocolFamily family, XmlNode node, string ns)
        {
            var familyElement = (XmlElement) node;
            foreach (XmlNode protocolNode in familyElement.ChildNodes)
            {
                if (protocolNode.NodeType != XmlNodeType.Element)
                    continue;
                var protocol = (XmlElement) protocolNode;
                string protocolName = protocol.GetAttribute("name");
                //从程序集中找到该协议的Class
                string className = string.Format("{0}{1}", ns, protocolName);
                //鉴于协议名有可能使用～等特殊符号开头，因此协议名无法做为类名
                //因此增加下面的节点判断，如果有classname属性则使用该属性做为类名，没有时使用name属性做类名
                string protocalClassName = protocol.GetAttribute("classname");
                if (!string.IsNullOrEmpty(protocalClassName))
                {
                    className = string.Format("{0}{1}", ns, protocalClassName);
                }

                Type protocolType = null;
                try
                {
                    protocolType = UtilityType.FindType(className);
                }
                catch (Exception exc)
                {
                    string msg = string.Format("族({0}):{1}类型载入异常。{2}", family.Family, className, exc.Message);
                    _Logger.Warn(msg, exc);
                }
                if (protocolType != null)
                {
                    ParseProtocolTools(family, protocolName, protocol);
                    //将该协议Class添入家族MAP中
                    family.Add(protocolName, protocolType);
                    //本协议的Content
                    XmlNode contentNode = protocol.SelectSingleNode("Content");
                    Type contentType = family.DefaultContentType;
                    if (contentNode != null)
                        contentType = UtilityType.FindType(contentNode.InnerText);
                    ProtocolContentMap.TryAdd(family.Family + protocolName, contentType);
                }
            }
        }

        /// <summary>解析协议的工具接口集,如果未发现定义协议的工具,将使用家族的默认工具
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="command">The command.</param>
        /// <param name="node">The node.</param>
        /// <param name="protocolElement"></param>
        private void ParseProtocolTools(ProtocolFamily family, string command, XmlElement protocolElement)
        {
            string key = family.Family + command;
            ProtocolTools defaults = family.DefaultTools;
            if (!protocolElement.HasChildNodes)
            {
                ProtocolToolsMap.TryAdd(key, defaults);
            }
            else
            {
                // **** 进入自定义工具的解析 ****
                ProtocolTools tools = defaults.CloneTools();
                var t = (XmlElement) protocolElement.SelectSingleNode("tools");
                tools.Parse(t);
                ProtocolToolsMap.TryAdd(key, tools);
            }
        }
    }
}