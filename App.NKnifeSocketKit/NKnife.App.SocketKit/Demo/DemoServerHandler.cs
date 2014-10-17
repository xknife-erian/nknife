using System;
using NKnife.Adapters;
using NKnife.App.SocketKit.Common;
using NKnife.Collections;
using NKnife.Interface;
using NKnife.Protocol;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Demo
{
    internal class DemoServerHandler : KnifeSocketProtocolHandler
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        private readonly AsyncObservableCollection<SocketMessage> _SocketMessages;

        public DemoServerHandler(AsyncObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
        }

        public override void Recevied(ISocketSession session, IProtocol protocol)
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