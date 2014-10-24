using System;
using NKnife.Adapters;
using NKnife.Collections;
using NKnife.Interface;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoClientHandler : KnifeSocketClientProtocolHandler
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        public override KnifeSocketSession Session { get; set; }

        private readonly AsyncObservableCollection<SocketMessage> _SocketMessages;

        public DemoClientHandler(AsyncObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
        }

        public override void Recevied(KnifeSocketSession session, StringProtocol protocol)
        {
            var msg = new SocketMessage();
            msg.Command = protocol.Command;
            msg.SocketDirection = SocketDirection.Receive;
            msg.Message = protocol.Generate();
            msg.Time = DateTime.Now.ToString("HH:mm:ss.fff");
            _SocketMessages.Insert(0, msg);
            _logger.Info("新消息解析完成" + msg.Message);
        }

    }
}
