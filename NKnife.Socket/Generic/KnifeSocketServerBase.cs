using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerBase : IKnifeSocketServer
    {
        public abstract ISocketServerConfig Config { get; }
        public abstract void Dispose();

        void IKnifeServer<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec,
            IProtocolFamily<string> protocolFamily,
            params IProtocolHandler<EndPoint, Socket, string>[] handlers)
        {
            Bind((KnifeSocketCodec) codec, (KnifeSocketProtocolFamily) protocolFamily,
                (KnifeSocketProtocolHandler[]) handlers);
        }

        ITunnelConfig IKnifeServer<EndPoint, Socket, string>.Config
        {
            get { return Config; }
        }

        void IKnifeServer<EndPoint, Socket, string>.AddFilter(ITunnelFilter<EndPoint, Socket, string> filter)
        {
            AddFilter((KnifeSocketServerFilter) filter);
        }

        public abstract bool Start();
        public abstract bool ReStart();
        public abstract bool Stop();
        public abstract void Configure(IPAddress ipAddress, int port);
        public abstract void AddFilter(KnifeSocketServerFilter filter);

        public abstract void Bind(KnifeSocketCodec codec, KnifeSocketProtocolFamily family,
            params KnifeSocketProtocolHandler[] handlers);
    }
}