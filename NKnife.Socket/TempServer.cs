using NKnife.Socket.Filters;
using NKnife.Socket.Interfaces;

namespace NKnife.Socket
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
