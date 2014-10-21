using System.Net;
using System.Net.Sockets;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class TunnelBase : ITunnel<EndPoint, Socket, string>
    {
        protected KnifeSocketServerConfig _Config = DI.Get<KnifeSocketServerConfig>();

        protected ITunnelFilterChain<EndPoint, Socket> _FilterChain;
        protected KnifeSocketCodec _Codec;
        protected StringProtocolFamily _Family;
        protected KnifeSocketServerProtocolHandler[] _Handlers;

        protected IPAddress _IpAddress;
        protected int _Port;

        public abstract ISocketConfig Config { get; set; }
        public abstract void Dispose();
        protected abstract void SetFilterChain();

        void ITunnel<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec, IProtocolFamily<string> protocolFamily,
            params IProtocolHandler<EndPoint, Socket, string>[] handlers)
        {
            var hs = new KnifeSocketServerProtocolHandler[handlers.Length];
            for (int i = 0; i < handlers.Length; i++)
            {
                hs[i] = (KnifeSocketServerProtocolHandler) handlers[i];
            }
            Bind((KnifeSocketCodec) codec, (StringProtocolFamily) protocolFamily, hs);
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

        public virtual void AddFilter(KnifeSocketFilter filter)
        {
            filter.BindGetter(() => _Codec, () => _Handlers, () => _Family);

            if (_FilterChain == null)
                SetFilterChain();
            if (_FilterChain != null)
                _FilterChain.AddLast(filter);
        }

        public virtual void Bind(KnifeSocketCodec codec, StringProtocolFamily protocolFamily,
            params KnifeSocketServerProtocolHandler[] handlers)
        {
            _Codec = codec;
            _Family = protocolFamily;
            _Handlers = handlers;
            OnBound();
        }

        protected abstract void OnBound();
    }
}