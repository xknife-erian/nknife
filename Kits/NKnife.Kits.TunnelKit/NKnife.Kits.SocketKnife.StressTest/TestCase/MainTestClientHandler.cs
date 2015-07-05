using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestClientHandler : BaseProtocolHandler<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MainTestClientHandler>();
        private bool _OnSending;
        private int _TimerInterval;
        public override List<string> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<string> protocol)
        {
            string command = protocol.Command;
            string message = _Family.Generate(protocol);
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            _logger.Debug(string.Format("client[收到] <== {0},{1},{2}", time, command, message));
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
            var command = "test";
            var protocol = _Family.Build(command);
            while (_OnSending)
            {
                try
                {
                    protocol.CommandParam = DateTime.Now.ToString("HHmmss");
                    WriteToAllSession(protocol);
                    string message = _Family.Generate(protocol);
                    string time = DateTime.Now.ToString("HH:mm:ss.fff");
                    _logger.Debug(string.Format("client[发出] <== {0},{1},{2}", time, command, message));
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
