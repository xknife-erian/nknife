using System;
using System.IO;
using System.Xml;
using NKnife.Adapters;
using NKnife.Interface;

namespace NKnife.Protocol.Generic.CommandParsers
{
    /// <summary>
    /// 一个基本的从协议字符串中解析命令字的工具
    /// </summary>
    public class DatagramCommandParser : StringProtocolCommandParser
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();
        
        public override string GetCommand(string protocolString)
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
                    _logger.Warn(string.Format("无法解析的协议字符串。{0}，异常：{1}", protocolString, e));
                }
            }
            return command;
        }
    }
}
