using System;

namespace SocketKnife.Interfaces
{
    public interface IProtocolHandler
    {
        void Bind(Action<ISocketSession, byte[]> sendMethod);
    }
}