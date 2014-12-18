using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace NKnife.Protocol.Generic
{
    public abstract class KnifeProtocolHandlerBase<TData, TSessionId, T> : IProtocolHandler<TData, TSessionId, T>
    {
        public ISessionProvider<TData, TSessionId> SessionProvider { get; set; } 
        public List<T> Commands { get; set; }

        public abstract void Recevied(TSessionId sessionId, IProtocol<T> protocol);
        public virtual void Write(TSessionId sessionId, TData data)
        {
            if (SessionProvider != null)
                SessionProvider.Send(sessionId, data);
        }

        public virtual void WriteAll(TData data)
        {
            if(SessionProvider !=null)
                SessionProvider.SendAll(data);
        }

        public abstract void Write(TSessionId sessionId, IProtocol<T> protocol);
        public abstract void WriteAll(IProtocol<T> protocol);
    }
}
