using System;
using NKnife.IoC;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeProtocolHandler : IProtocolHandler
    {
        protected KnifeProtocolHandler()
        {
            SessionMap = DI.Get<ISocketSessionMap>();
        }

        protected Action<ISocketSession, byte[]> _WriteBaseMethod;
        protected Action<ISocketSession, IProtocol> _WriteProtocolMethod;

        public virtual ISocketSessionMap SessionMap { get; private set; }

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

    class DemoHandler : KnifeProtocolHandler
    {
        public override void Recevied(ISocketSession session, IProtocol protocol)
        {
            switch (protocol.Command)
            {
                case "Call":
                    this.Write(session, new byte[] { });
                    break;
                case "GetTicket":
                    this.Write(session, protocol);
                    break;
            }
        }
    }
}