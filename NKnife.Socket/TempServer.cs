using NKnife.Socket;
using NKnife.Socket.Interfaces;
using SocketKnife.Filters;

namespace SocketKnife
{
    internal class TempServer
    {
        public TempServer()
        {
            ISocketServerKnife server = new TcpServerKnife();
            server.Bind("localhost", 11001);
            server.GetConfig.SetReadBufferSize(2048);
            server.GetFilterChain().AddLast("KeepConn", new KeepFilter(true));
            server.GetFilterChain().AddLast("Codec", new CodecFilter());
            //server.SetMessageHandler(new )
            server.Start();
            server.ReStart();
            server.Stop();
        }
    }
}
