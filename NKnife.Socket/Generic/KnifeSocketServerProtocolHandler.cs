using System;
using NKnife.Protocol.Generic;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerProtocolHandler : KnifeSocketProtocolHandler
    {
        public KnifeSocketSessionMap SessionMap { get; set; }

        public virtual void WriteAll(byte[] data)
        {
            foreach (var session in SessionMap.Values())
            {
                Write(session, data);
            }
        }

        public virtual void WriteAll(StringProtocol protocol)
        {
            foreach (var session in SessionMap.Values())
            {
                Write(session, protocol);
            }
        }
    }
}