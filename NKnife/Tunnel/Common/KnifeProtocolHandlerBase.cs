using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Common
{
    public abstract class KnifeProtocolHandlerBase<TSessionId, TData> : ITunnelProtocolHandler<TSessionId, TData, byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeProtocolHandlerBase<TSessionId, TData>>();

        #region Codec

        private ITunnelCodecBase<TData> _CodecBase;
        public ITunnelCodecBase<TData> Codec
        {
            get { return _CodecBase; }
            set { _CodecBase = value; }
        }
        ITunnelCodec<TData, byte[]> ITunnelProtocolHandler<TSessionId, TData, byte[]>.Codec
        {
            get { return _CodecBase; }
            set { _CodecBase = (ITunnelCodecBase<TData>) value; }
        }

        #endregion

        protected IProtocolFamily<TData> _Family;
        public abstract List<TData> Commands { get; set; }
        public abstract void Recevied(TSessionId sessionId, IProtocol<TData> protocol);
        public event EventHandler<SessionEventArgs<TSessionId>> OnSendToSession;
        public event EventHandler<EventArgs<byte[]>> OnSendToAll;

        public virtual void Bind(ITunnelCodecBase<TData> codec, IProtocolFamily<TData> protocolFamily)
        {
            _CodecBase = codec;
            _Family = protocolFamily;
        }

        /// <summary>
        ///     发送协议，帮助方法
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        public virtual void WriteToSession(TSessionId sessionId, IProtocol<TData> protocol)
        {
            try
            {
                var original = _Family.Generate(protocol);
                var data = _CodecBase.Encoder.Execute(original);
                var handler = OnSendToSession;
                if (handler != null)
                {
                    var session = new TunnelSession<TSessionId> {Id = sessionId, Data = data};
                    var e = new SessionEventArgs<TSessionId>(session);
                    handler.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("发送protocol异常,{0}", ex));
            }
        }

        public virtual void WriteToAllSession(IProtocol<TData> protocol)
        {
            try
            {
                var str = _Family.Generate(protocol);
                var data = _CodecBase.Encoder.Execute(str);
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