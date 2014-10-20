using System.Net;
using System.Net.Sockets;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class TunnelBase : ITunnel<EndPoint, Socket, string>
    {
        protected KnifeSocketServerConfig _Config = DI.Get<KnifeSocketServerConfig>();
        protected KnifeSocketServerFilterChain _FilterChain = DI.Get<KnifeSocketServerFilterChain>();
        protected KnifeSocketSessionMap _SessionMap = DI.Get<KnifeSocketSessionMap>();

        protected KnifeSocketCodec _Codec;
        protected KnifeSocketProtocolFamily _Family;
        protected KnifeSocketProtocolHandler[] _Handlers;

        protected IPAddress _IpAddress;
        protected int _Port;

        public abstract ISocketConfig Config { get; set; }
        public abstract void Dispose();

        void ITunnel<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec, IProtocolFamily<string> protocolFamily,
            params IProtocolHandler<EndPoint, Socket, string>[] handlers)
        {
            var hs = new KnifeSocketProtocolHandler[handlers.Length];
            for (int i = 0; i < handlers.Length; i++)
            {
                hs[i] = (KnifeSocketProtocolHandler) handlers[i];
            }
            Bind((KnifeSocketCodec) codec, (KnifeSocketProtocolFamily) protocolFamily, hs);
        }

        ITunnelConfig ITunnel<EndPoint, Socket, string>.Config
        {
            get { return Config; }
            set { Config = (ISocketConfig) value; }
        }

        void ITunnel<EndPoint, Socket, string>.AddFilter(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddFilter((KnifeSocketServerFilter)filter);
        }

        public abstract bool Start();
        public abstract bool ReStart();
        public abstract bool Stop();

        public virtual void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }

        public virtual void AddFilter(KnifeSocketServerFilter filter)
        {
            filter.Bind(GetFamily, GetHandle, GetSessionMap, GetCodec);
            _FilterChain.AddLast(filter);
        }

        public virtual void Bind(KnifeSocketCodec codec, KnifeSocketProtocolFamily protocolFamily,
            params KnifeSocketProtocolHandler[] handlers)
        {
            _Codec = codec;
            _Family = protocolFamily;
            _Handlers = handlers;
            foreach (KnifeSocketProtocolHandler handler in _Handlers)
            {
                handler.SessionMap = _SessionMap;
            }
        }

        private KnifeSocketSessionMap GetSessionMap()
        {
            return _SessionMap;
        }

        private KnifeSocketProtocolHandler[] GetHandle()
        {
            return _Handlers;
        }

        private KnifeSocketProtocolFamily GetFamily()
        {
            return _Family;
        }

        private KnifeSocketCodec GetCodec()
        {
            return _Codec;
        }
    }
}