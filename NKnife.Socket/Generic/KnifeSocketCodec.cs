using System.Collections.Generic;
using Ninject;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public class KnifeSocketCodec : ITunnelCodec<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public KnifeSocketCodec()
        {
            
        }

        public KnifeSocketCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        private KnifeSocketDatagramDecoder _SocketDecoder;
        private bool _HasSetSocketDecoder;
        public virtual KnifeSocketDatagramDecoder SocketDecoder
        {
            get
            {
                if (!_HasSetSocketDecoder)
                {
                    return string.IsNullOrEmpty(CodecName)
                        ? DI.Get<KnifeSocketDatagramDecoder>()
                        : DI.Get<KnifeSocketDatagramDecoder>(CodecName);
                }
                return _SocketDecoder;
            }
            set
            {
                _SocketDecoder = value;
                _HasSetSocketDecoder = true;
            }
        }

        private KnifeSocketDatagramEncoder _SocketEncoder;
        private bool _HasSetSocketEncoder;

        public virtual KnifeSocketDatagramEncoder SocketEncoder
        {
            get
            {
                if (!_HasSetSocketEncoder)
                {
                    return string.IsNullOrEmpty(CodecName) ? DI.Get<KnifeSocketDatagramEncoder>() : DI.Get<KnifeSocketDatagramEncoder>(CodecName);
                }
                return _SocketEncoder;
            }
            set
            {
                _SocketEncoder = value;
                _HasSetSocketEncoder = true;
            }
        }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return SocketDecoder; }
            set
            {
                SocketDecoder = (KnifeSocketDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return SocketEncoder; }
            set
            {
                SocketEncoder = (KnifeSocketDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}
