using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServerHandler : KnifeSocketServerProtocolHandler
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        private readonly ObservableCollection<SocketMessage> _SocketMessages;

        public DemoServerHandler(ObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
        }

        public override void Recevied(KnifeSocketSession session, StringProtocol protocol)
        {
            var socketMessage = new SocketMessage
            {
                Command = protocol.Command, 
                SocketDirection = SocketDirection.Receive, 
                Message = protocol.Generate(), 
                Time = DateTime.Now.ToString("HH:mm:ss.fff")
            };
            _logger.Info("����Ϣ�������" + socketMessage.Message);

            try
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                    return;
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    AddSocketMessage(socketMessage);
                }
                else
                {
                    var socketDelegate = new SocketMessageInserter(AddSocketMessage);
                    Application.Current.Dispatcher.BeginInvoke(socketDelegate, new object[] { socketMessage });
                }
            }
            catch (Exception e)
            {
                string error = string.Format("��ؼ�дSocket��Ϣ�����쳣.{0}{1}", e.Message, e.StackTrace);
                Debug.Fail(error);
            }
        }

        protected void AddSocketMessage(SocketMessage socketMessage)
        {
            _SocketMessages.Insert(0, socketMessage);
        }

        private delegate void SocketMessageInserter(SocketMessage socketMessage);
    }
}