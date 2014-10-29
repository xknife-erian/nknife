using NKnife.Protocol.Generic;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientProtocolHandler : KnifeSocketProtocolHandler
    {
        public KnifeSocketSession Session { get; set; } //��OnBound�����б���ֵ

        public virtual void Write(byte[] data)
        {
            _WriteBaseMethod.Invoke(Session, data);
        }

        public virtual void Write(StringProtocol data)
        {
            _WriteProtocolMethod.Invoke(Session, data);
        }
    }
}