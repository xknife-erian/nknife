using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic.Families;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerBase : IKnifeSocketServer
    {
        public abstract ISocketServerConfig Config { get; }
        public abstract void Dispose();

        void IKnifeServer<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec, IProtocolFamily protocolFamily,
            IProtocolHandler<EndPoint, Socket> handler)
        {
            Bind((KnifeSocketCodec)codec, (KnifeProtocolFamily)protocolFamily, (KnifeSocketProtocolHandler)handler);
        }

        ITunnelConfig IKnifeServer<EndPoint, Socket, string>.Config
        {
            get { return Config; }
        }

        void IKnifeServer<EndPoint, Socket, string>.AddFilter(ITunnelFilter<EndPoint, Socket, string> filter)
        {
            AddFilter((KnifeSocketServerFilter)filter);
        }

        public abstract bool Start();
        public abstract bool ReStart();
        public abstract bool Stop();
        public abstract void Configure(IPAddress ipAddress, int port);
        public abstract void AddFilter(KnifeSocketServerFilter filter);
        public abstract void Bind(KnifeSocketCodec codec, KnifeProtocolFamily family, KnifeSocketProtocolHandler handle);
    }
}