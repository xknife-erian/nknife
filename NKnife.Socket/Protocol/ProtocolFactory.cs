using System;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Protocol
{
    /// <summary>Socket通讯协议工厂
    /// </summary>
    public abstract class ProtocolFactory
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        /// <summary>设置协议的配置管理器
        /// </summary>
        /// <value>
        /// The protocol setting.
        /// </value>
        protected abstract ProtocolSetting ProtocolSetting { get; }

        /// <summary>根据通讯协议的协议族字符串与命令字快速创建协议对象
        /// </summary>
        /// <param name="family">协议族字符串</param>
        /// <param name="command">命令字</param>
        /// <returns></returns>
        public IProtocol Get(string family, string command)
        {
            IProtocol protocol = null;
            try
            {
                ProtocolFamily pfamily;
                if (!ProtocolSetting.FamilyMap.TryGetValue(family, out pfamily))
                {
                    _logger.Warn(string.Format("协议族缓存中没有对应的协议族({0})。", family));
                    return null;
                }
                Type type;
                if (!pfamily.TryGetValue(command, out type))
                {
                    _logger.Warn(string.Format("协议族没有对应命令字({0})的协议。", command));
                    return null;
                }

                //创建协议的实例
                protocol = (IProtocol) UtilityType.CreateObject(type, typeof (IProtocol), true);

                //协议的工具集
                string toolKey = string.Format("{0}{1}", pfamily.Family, command);
                ProtocolTools tools;
                if (ProtocolSetting.ProtocolToolsMap.TryGetValue(toolKey, out tools))
                    protocol.Tools = tools;

                //协议的内容容器
                protocol.Content = GetBlankContent(family, command);

                return protocol;
            }
            catch (Exception e)
            {
                _logger.Warn("从协议工厂实例协议异常。", e);
                return protocol;
            }
        }

        /// <summary>根据命令字与协议族获取指定的协议的协议内容体
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected IProtocolContent GetBlankContent(string family, string command)
        {
            Type type;
            if (ProtocolSetting.ProtocolContentMap.TryGetValue(family + command, out type))
            {
                try
                {
                    var c = (IProtocolContent) Activator.CreateInstance(type);
                    c.Command = command;
                    return c;
                }
                catch (Exception e)
                {
                    _logger.Warn("创建协议内容容器异常。", e);
                    return null;
                }
            }
            return null;
        }
    }
}