using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife
{
    class TempServer
    {
        public TempServer()
        {
            ISocketServerKnife server = new TcpServerKnife();
            server.Bind("localhost", 11001);
            server.GetConfig.SetReadBufferSize(2048);
            server.GetConfig.SetIdleTime(IdleStatus.BOTH_IDLE, 10);
            server.GetFilterChain().AddLast("KeepConn", new KeepFilter(true))
                .GetFilterChain().AddLast("Codec", new ProtocolCodecFilter())
                .GetFilterChain().AddLast("logger", new LoggingFilter());
            server.Start();
            server.ReStart();
            server.Stop();
        }
    }

    internal interface ISocketServerKnife
    {
        void Bind(string localhost, int port);
        ISocketConfig GetConfig { get; }
        void GetFilterChain();
    }

    internal interface ISocketConfig
    {
        void SetReadBufferSize(int size);
        void SetIdleTime(object bothIdle, int i);
    }
}
