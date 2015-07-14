﻿using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class MockClientHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MockClientHandler>();
        private bool _OnSending;
        private int _TimerInterval;
        public override List<byte[]> Commands { get; set; }

        public EventHandler<NangleProtocolEventArgs> ProtocolReceived;

        private void OnProtocolReceived(IProtocol<byte[]> protocol)
        {
            var handler = ProtocolReceived;
            if (handler != null)
            {
                handler.Invoke(this,new NangleProtocolEventArgs
                {
                    Protocol = (BytesProtocol)protocol,
                });
            }
        }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            byte[] command = protocol.Command;
            byte[] message = _Family.Generate(protocol);
            //_logger.Debug(string.Format("client[收到]<==[{0}], {1}", command.ToHexString(), message.ToHexString()));
            OnProtocolReceived(protocol);
        }

        public void StartSendingTimer(int timerInterval = 1000)
        {
            _logger.Info("启动客户端定时发送模拟");
            _TimerInterval = timerInterval;
            _OnSending = true;
            var sendThread = new Thread(SendLoop);
            sendThread.Start();
        }

        private void SendLoop()
        {
            var command = new byte[]{0x00,0x01};
            var protocol = _Family.Build(command);
            protocol.CommandParam = new byte[]{0x30,0x31,0x32,0x33};
            while (_OnSending)
            {
                try
                {
                    WriteToAllSession(protocol);
                    var message = _Family.Generate(protocol);
                    //_logger.Debug(string.Format("client[发出]==>[{0}], {1}", command.ToHexString(), message.ToHexString()));
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("client send exception: {0}",ex.Message));
                }
                Thread.Sleep(_TimerInterval);
            }
        }

        public void StopSendingTimer()
        {
            _logger.Info("停止客户端定时发送模拟");
            _OnSending = false;
        }
    }
}