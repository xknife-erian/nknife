using System;
using NKnife.Protocol.Generic;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientProtocolHandler : KnifeSocketProtocolHandler
    {
        public Func<KnifeSocketSession> SessionGetter { get; set; } //在OnBound函数中被赋值

        public virtual void Write(byte[] data)
        {
            _WriteBaseMethod.Invoke(SessionGetter.Invoke(), data);
        }

        public virtual void Write(StringProtocol data)
        {
            _WriteProtocolMethod.Invoke(SessionGetter.Invoke(), data);
        }

        public override void Recevied(KnifeSocketSession session, StringProtocol protocol)
        {
            Received(protocol);
        }

        public abstract void Received(StringProtocol protocol);
    }
}