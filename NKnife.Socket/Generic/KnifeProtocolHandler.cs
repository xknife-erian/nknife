using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeProtocolHandler : IProtocolHandler
    {
        protected Action<ISocketSession, byte[]> _SendMethod;

        public void Bind(Action<ISocketSession, byte[]> sendMethod)
        {
            _SendMethod = sendMethod;
        }

        public virtual void Send(ISocketSession session, byte[] data)
        {
            _SendMethod.Invoke(session, data);
        }
    }
}
