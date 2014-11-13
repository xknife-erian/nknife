using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Common.Logging;
using NKnife.Collections;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.NLog3.Controls.WPF;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServerHandler : KnifeSocketServerProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private readonly ObservableCollection<SocketMessage> _SocketMessages;

        public DemoServerHandler(ObservableCollection<SocketMessage> socketMessages, Dispatcher dispatcher)
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
            _logger.Info("新消息解析完成" + socketMessage.Message);

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
                string error = string.Format("向控件写Socket消息发生异常.{0}{1}", e.Message, e.StackTrace);
                Debug.Fail(error);
            }
        }

        protected void AddSocketMessage(SocketMessage logEvent)
        {
            _SocketMessages.Insert(0, logEvent);
        }

        private delegate void SocketMessageInserter(SocketMessage socketMessage);
    }
}