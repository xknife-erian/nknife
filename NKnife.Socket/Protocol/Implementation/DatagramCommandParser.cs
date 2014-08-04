using System;
using System.IO;
using System.Xml;
using NLog;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife.Protocol.Implementation
{
    /// <summary>
    ///     一个基本的从协议字符串中解析命令字的工具
    /// </summary>
    public class DatagramCommandParser : IDatagramCommandParser
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public string GetCommand(string protocolString)
        {
            string command = string.Empty;
            if (protocolString.IndexOf("KeepAliveTestFromServer", System.StringComparison.Ordinal) >= 0)
            {
                return "KeepAliveTestFromServer";
            }
            using (var reader = new XmlTextReader(new StringReader(protocolString)))
            {
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            command = reader.Name;
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    _Logger.Warn("无法解析的协议字符串。{0}，异常：{1}", protocolString, e);
                }
            }
            return command;
        }
    }
}