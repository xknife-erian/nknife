using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;

namespace NKnife.Protocol.Generic
{
    public abstract class KnifeProtocolHandlerBase<TSessionId, TOriginal> : IProtocolHandler<byte[], TSessionId, TOriginal>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeProtocolHandlerBase<TSessionId, TOriginal>>();

        protected ITunnelCodec<byte[], TOriginal> _Codec;
        protected IProtocolFamily<TOriginal> _Family;
        public abstract List<TOriginal> Commands { get; set; }
        public abstract void Recevied(TSessionId sessionId, IProtocol<TOriginal> protocol);
        public event EventHandler<SessionEventArgs<byte[], TSessionId>> OnSendToSession;
        public event EventHandler<EventArgs<byte[]>> OnSendToAll;

        public virtual void Bind(ITunnelCodec<byte[], TOriginal> codec, IProtocolFamily<TOriginal> protocolFamily)
        {
            _Codec = codec;
            _Family = protocolFamily;
        }

        /// <summary>
        ///     发送协议，帮助方法
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        public virtual void WriteToSession(TSessionId sessionId, IProtocol<TOriginal> protocol)
        {
            try
            {
                var original = _Family.Generate(protocol);
                var data = _Codec.Encoder.Execute(original);
                var handler = OnSendToSession;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs<byte[], TSessionId>(new TunnelSession<byte[], TSessionId>
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

        public virtual void WriteToAllSession(IProtocol<TOriginal> protocol)
        {
            try
            {
                var str = _Family.Generate(protocol);
                var data = _Codec.Encoder.Execute(str);
                var handler = OnSendToAll;
                if (handler != null)
                {
                    handler.Invoke(this, new EventArgs<byte[]>(data));
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }
    }
}