using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class KnifeStringCodec : ITunnelCodec<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public KnifeStringCodec()
        {
            
        }

        public KnifeStringCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        private KnifeStringDatagramDecoder _StringDecoder;
        private bool _HasSetSocketDecoder;
        public virtual KnifeStringDatagramDecoder StringDecoder
        {
            get
            {
                if (!_HasSetSocketDecoder)
                {
                    return string.IsNullOrEmpty(CodecName)
                        ? DI.Get<KnifeStringDatagramDecoder>()
                        : DI.Get<KnifeStringDatagramDecoder>(CodecName);
                }
                return _StringDecoder;
            }
            set
            {
                _StringDecoder = value;
                _HasSetSocketDecoder = true;
            }
        }

        private KnifeStringDatagramEncoder _StringEncoder;
        private bool _HasSetSocketEncoder;

        public virtual KnifeStringDatagramEncoder StringEncoder
        {
            get
            {
                if (!_HasSetSocketEncoder)
                {
                    return string.IsNullOrEmpty(CodecName) ? DI.Get<KnifeStringDatagramEncoder>() : DI.Get<KnifeStringDatagramEncoder>(CodecName);
                }
                return _StringEncoder;
            }
            set
            {
                _StringEncoder = value;
                _HasSetSocketEncoder = true;
            }
        }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return StringDecoder; }
            set
            {
                StringDecoder = (KnifeStringDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return StringEncoder; }
            set
            {
                StringEncoder = (KnifeStringDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}
