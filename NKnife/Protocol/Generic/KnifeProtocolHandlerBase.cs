using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Events;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;

namespace NKnife.Protocol.Generic
{
    public abstract class KnifeProtocolHandlerBase<TData, TSessionId, T> : IProtocolHandler<TData, TSessionId, T>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeProtocolHandlerBase<TData, TSessionId, T>>();
        public abstract List<T> Commands { get; set; }
        public abstract void Recevied(TSessionId sessionId, IProtocol<T> protocol);
        public event EventHandler<SessionEventArgs<TData, TSessionId>> OnSendToSession;
        public event EventHandler<EventArgs<TData>> OnSendToAll;

        protected ITunnelCodec<T, TData> Codec;
        protected IProtocolFamily<T> Family;

        public virtual void Bind(ITunnelCodec<T,TData> codec, IProtocolFamily<T> protocolFamily)
        {
            Codec = codec;
            Family = protocolFamily;
        }

        /// <summary>
        /// 发送协议，帮助方法
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        public virtual void WriteToSession(TSessionId sessionId, IProtocol<T> protocol)
        {
            try
            {
                T str = Family.Generate(protocol);
                TData data = Codec.Encoder.Execute(str);
                var handler = OnSendToSession;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs<TData, TSessionId>(new TunnelSession<TData, TSessionId>
                    {
                        Id = sessionId,
                        Data = data
                    }));
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }

        public virtual void WriteToAllSession(IProtocol<T> protocol)
        {
            try
            {
                T str = Family.Generate(protocol);
                TData data = Codec.Encoder.Execute(str);
                var handler = OnSendToAll;
                if (handler != null)
                {
                    handler.Invoke(this, new EventArgs<TData>(data));
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }
    }
}
