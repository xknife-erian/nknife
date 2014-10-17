using System;
using System.Net;
using System.Net.Sockets;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeProtocolHandler : IProtocolHandler<EndPoint, Socket>
    {
        protected Action<ISocketSession, byte[]> _WriteBaseMethod;
        protected Action<ISocketSession, IProtocol> _WriteProtocolMethod;

        public virtual ITunnelSessionMap<EndPoint, Socket> SessionMap { get; set; }

        void IProtocolHandler<EndPoint, Socket>.Recevied(ITunnelSession<EndPoint, Socket> session, IProtocol protocol)
        {
            Recevied((ISocketSession)session, protocol);
        }

        void IProtocolHandler<EndPoint, Socket>.Write(ITunnelSession<EndPoint, Socket> session, byte[] data)
        {
            Write((ISocketSession)session, data);
        }

        void IProtocolHandler<EndPoint, Socket>.Write(ITunnelSession<EndPoint, Socket> session, IProtocol data)
        {
            Write((ISocketSession)session, data);
        }

        public virtual void Bind(Action<ISocketSession, byte[]> sendMethod)
        {
            _WriteBaseMethod = sendMethod;
        }

        public virtual void Bind(Action<ISocketSession, IProtocol> sendMethod)
        {
            _WriteProtocolMethod = sendMethod;
        }

        public abstract void Recevied(ISocketSession session, IProtocol protocol);

        public virtual void Write(ISocketSession session, byte[] data)
        {
            _WriteBaseMethod.Invoke(session, data);
        }

        public virtual void Write(ISocketSession session, IProtocol data)
        {
            _WriteProtocolMethod.Invoke(session, data);
        }

    }
}