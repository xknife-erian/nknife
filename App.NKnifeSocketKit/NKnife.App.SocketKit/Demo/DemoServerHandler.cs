using System;
using System.Collections.Generic;
using NKnife.Adapters;
using NKnife.App.SocketKit.Common;
using NKnife.Collections;
using NKnife.Interface;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Demo
{
    public class DemoServerHandler : KnifeSocketServerProtocolHandler
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        private readonly AsyncObservableCollection<SocketMessage> _SocketMessages;

        public DemoServerHandler(AsyncObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
        }

        public override KnifeSocketSessionMap SessionMap { get; set; }

        public override List<string> Commands { get; set; }

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