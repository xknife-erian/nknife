using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic.Protocols;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketProtocolHandler : IProtocolHandler<EndPoint, Socket, string>
    {
        protected Action<KnifeSocketSession, byte[]> _WriteBaseMethod;
        protected Action<KnifeSocketSession, KnifeSocketProtocol> _WriteProtocolMethod;
        public KnifeSocketSessionMap SessionMap { get; set; }
        public abstract List<string> Commands { get; set; }

        ITunnelSessionMap<EndPoint, Socket> IProtocolHandler<EndPoint, Socket, string>.SessionMap
        {
            get { return SessionMap; }
            set { SessionMap = (KnifeSocketSessionMap) value; }
        }

        void IProtocolHandler<EndPoint, Socket, string>.Recevied(ITunnelSession<EndPoint, Socket> session, IProtocol<string> protocol)
        {
            Recevied((KnifeSocketSession) session, (KnifeSocketProtocol) protocol);
        }

        void IProtocolHandler<EndPoint, Socket, string>.Write(ITunnelSession<EndPoint, Socket> session, byte[] data)
        {
            Write((KnifeSocketSession) session, data);
        }

        void IProtocolHandler<EndPoint, Socket, string>.Write(ITunnelSession<EndPoint, Socket> session, IProtocol<string> data)
        {
            Write((KnifeSocketSession) session, (KnifeSocketProtocol) data);
        }

        public virtual void Bind(Action<KnifeSocketSession, byte[]> sendMethod)
        {
            _WriteBaseMethod = sendMethod;
        }

        public virtual void Bind(Action<KnifeSocketSession, KnifeSocketProtocol> sendMethod)
        {
            _WriteProtocolMethod = sendMethod;
        }

        public abstract void Recevied(KnifeSocketSession session, KnifeSocketProtocol protocol);

        public virtual void Write(KnifeSocketSession session, byte[] data)
        {
            _WriteBaseMethod.Invoke(session, data);
        }

        public virtual void Write(KnifeSocketSession session, KnifeSocketProtocol data)
        {
            _WriteProtocolMethod.Invoke(session, data);
        }
    }
}