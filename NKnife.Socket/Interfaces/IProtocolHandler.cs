using System;

namespace SocketKnife.Interfaces
{
    public interface IProtocolHandler
    {
        ISocketSessionMap SessionMap { get; set; }

        void Bind(Action<ISocketSession, byte[]> sendMethod);
        void Bind(Action<ISocketSession, IProtocol> sendMethod);

        void Recevied(ISocketSession session, IProtocol protocol);

        void Write(ISocketSession session, byte[] data);

        void Write(ISocketSession session, IProtocol data);
    }
}