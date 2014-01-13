using System;
using System.Xml;
using System.IO;
using Gean.Network.Interfaces;

namespace Gean.Network.Protocol.Implementation
{
    /// <summary>
    /// 一个基本的从协议字符串中解析命令字的工具
    /// </summary>
    public class DatagramCommandParser : IDatagramCommandParser
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();
        
        public string GetCommand(string protocolString)
        {
            string command = string.Empty;
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
                    _Logger.Warn(string.Format("无法解析的协议字符串。{0}，异常：{1}", protocolString, e));
                }
            }
            return command;
        }
    }
}
