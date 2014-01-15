using System;
using Gean;
using NKnife.Extensions;
using NKnife.NetWork.Config;
using NKnife.NetWork.Interfaces;
using NKnife.Utility;
using NLog;

namespace NKnife.NetWork.Protocol
{
    /// <summary>Socket通讯协议工厂
    /// </summary>
    public static class Protocols
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>根据通讯协议的协议族字符串与命令字快速创建协议对象
        /// </summary>
        /// <param name="family">协议族</param>
        /// <param name="command">命令字</param>
        /// <returns></returns>
        public static IProtocol Factory(ProtocolFamilyType family, string command)
        {
            return Factory(family.ToString(), command);
        }

        /// <summary>根据通讯协议的协议族字符串与命令字快速创建协议对象
        /// </summary>
        /// <param name="family">协议族字符串</param>
        /// <param name="command">命令字</param>
        /// <returns></returns>
        public static IProtocol Factory(string family, string command)
        {
            IProtocol protocol = null;
            try
            {
                ProtocolFamily pfamily;
                if (!ProtocolSetting.ME.FamilyMap.TryGetValue(family, out pfamily))
                {
                    _Logger.Warn(string.Format("协议族缓存中没有对应的协议族({0})。", family));
                    return null;
                }
                Type type;
                if (!pfamily.TryGetValue(command, out type))
                {
                    _Logger.Warn(string.Format("协议族没有对应命令字({0})的协议。", command));
                    return null;
                }
                //协议
                protocol = (IProtocol)UtilityType.CreateObject(type, typeof(IProtocol), true);
                //协议的工具集
                string toolKey = string.Format("{0}{1}", pfamily.Family, command);
                ProtocolTools tools = null;
                if (ProtocolSetting.ME.ProtocolToolsMap.TryGetValue(toolKey, out tools))
                    protocol.Tools = tools;
                return protocol;
            }
            catch (Exception e)
            {
                _Logger.ErrorE("从协议工厂初例协议异常。",e);
                return protocol;
            }
        }

        /// <summary>根据命令字与协议族获取指定的协议的协议内容体
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static IProtocolContent GetBlankContent(string family, string command)
        {
            Type type;
            if (ProtocolSetting.ME.ProtocolContentMap.TryGetValue(family + command, out type))
            {
                try
                {
                    var c = (IProtocolContent)Activator.CreateInstance(type);
                    c.Command = command;
                    return c;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}