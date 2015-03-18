﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using Common.Logging;
using NKnife.Collections;
using NKnife.Interface;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoClientHandler : KnifeProtocolHandlerBase<byte[], EndPoint, string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private StringProtocolFamily _Family;
        private readonly ObservableCollection<SocketMessage> _SocketMessages;

        public DemoClientHandler(StringProtocolFamily family, ObservableCollection<SocketMessage> socketMessages)
        {
            _Family = family;
            _SocketMessages = socketMessages;
        }

        public override List<string> Commands { get; set; }

        public override void Recevied(EndPoint sessionId, IProtocol<string> protocol)
        {
            var msg = new SocketMessage();
            msg.Command = protocol.Command;
            msg.SocketDirection = SocketDirection.Receive;
            msg.Message = _Family.Generate((StringProtocol) protocol);
            msg.Time = DateTime.Now.ToString("HH:mm:ss.fff");
            _logger.Info("新消息解析完成" + msg.Message);

            try
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                    return;
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    AddSocketMessage(msg);
                }
                else
                {
                    var socketDelegate = new SocketMessageInserter(AddSocketMessage);
                    Application.Current.Dispatcher.BeginInvoke(socketDelegate, new object[] { msg });
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

    class MyClass
    {
        public MyClass()
        {
            KnifeProtocolHandlerBase<byte[], EndPoint, string> handler = new DemoClientHandler(null, null);
            handler.WriteToAllSession(new StringProtocol());
        } 
    }
}
