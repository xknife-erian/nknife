using System;
using NKnife.App.SocketKit.Common;
using NKnife.Collections;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Mvvm.ViewModels
{
    internal class DemoServerHandler : KnifeProtocolHandler
    {
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
        }
    }
}