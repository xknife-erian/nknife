using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using Common.Logging;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServerHandler : KnifeProtocolHandlerBase<EndPoint, string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<DemoServerHandler>();
        private StringProtocolFamily _Family;
        private readonly ObservableCollection<SocketMessage> _SocketMessages;

        public DemoServerHandler(StringProtocolFamily family, ObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
            _Family = family;
        }

        public override List<string> Commands { get; set; }

        public override void Recevied(EndPoint sessionId, IProtocol<string> protocol)
        {
            var socketMessage = new SocketMessage
            {
                Command = protocol.Command, 
                SocketDirection = SocketDirection.Receive,
                Message = _Family.Generate((StringProtocol) protocol), 
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

        protected void AddSocketMessage(SocketMessage socketMessage)
        {
            _SocketMessages.Insert(0, socketMessage);
        }

        private delegate void SocketMessageInserter(SocketMessage socketMessage);


    }
}