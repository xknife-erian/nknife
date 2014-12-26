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
        private bool _HasSetDecoder;
        public virtual KnifeStringDatagramDecoder StringDecoder
        {
            get
            {
                if (!_HasSetDecoder)
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
                _HasSetDecoder = true;
            }
        }

        private KnifeStringDatagramEncoder _StringEncoder;
        private bool _HasSetEncoder;

        public virtual KnifeStringDatagramEncoder StringEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                {
                    return string.IsNullOrEmpty(CodecName) ? DI.Get<KnifeStringDatagramEncoder>() : DI.Get<KnifeStringDatagramEncoder>(CodecName);
                }
                return _StringEncoder;
            }
            set
            {
                _StringEncoder = value;
                _HasSetEncoder = true;
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
