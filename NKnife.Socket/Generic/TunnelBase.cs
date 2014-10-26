using System.Net;
using System.Net.Sockets;
using NKnife.Adapters;
using NKnife.Interface;
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
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected ITunnelFilterChain<EndPoint, Socket> _FilterChain;
        protected KnifeSocketCodec _Codec;
        protected StringProtocolFamily _Family;
        protected KnifeSocketProtocolHandler[] _Handlers;

        protected IPAddress _IpAddress;
        protected int _Port;

        public abstract KnifeSocketConfig Config { get; set; }
        public abstract void Dispose();
        protected abstract void SetFilterChain();

        void ITunnel<EndPoint, Socket, string>.Bind(ITunnelCodec<string> codec, IProtocolFamily<string> protocolFamily,
            params IProtocolHandler<EndPoint, Socket, string>[] handlers)
        {
            var hs = new KnifeSocketProtocolHandler[handlers.Length];
            for (int i = 0; i < handlers.Length; i++)
            {
                hs[i] = (KnifeSocketProtocolHandler) handlers[i];
            }
            Bind((KnifeSocketCodec) codec, (StringProtocolFamily) protocolFamily, hs);
        }

        ITunnelConfig ITunnel<EndPoint, Socket, string>.Config
        {
            get { return Config; }
            set { Config = (KnifeSocketConfig)value; }
        }

        void ITunnel<EndPoint, Socket, string>.AddFilter(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddFilter((KnifeSocketFilter)filter);
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
            params KnifeSocketProtocolHandler[] handlers)
        {
            _Codec = codec;
            _logger.Info(string.Format("绑定Codec成功。{0},{1}", _Codec.SocketDecoder.GetType().Name, _Codec.SocketEncoder.GetType().Name));

            _Handlers = handlers;
            _logger.Info(string.Format("绑定{0}个Handler成功。", _Handlers.Length));

            _Family = protocolFamily;
            _logger.Info(string.Format("协议族[{0}]中有{1}个协议绑定成功。", _Family.FamilyName, _Family.Count));

            OnBound();
        }

        protected abstract void OnBound();
    }
}