using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class KnifeStringCodec : ITunnelCodec<string, byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeStringCodec>();
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;
        private KnifeStringDatagramDecoder _StringDecoder;
        private KnifeStringDatagramEncoder _StringEncoder;

        public KnifeStringCodec()
        {
        }

        public KnifeStringCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual KnifeStringDatagramDecoder StringDecoder
        {
            get
            {
                if (!_HasSetDecoder)
                {
                    return string.IsNullOrEmpty(CodecName) ? DI.Get<KnifeStringDatagramDecoder>() : DI.Get<KnifeStringDatagramDecoder>(CodecName);
                }
                return _StringDecoder;
            }
            set
            {
                _StringDecoder = value;
                _HasSetDecoder = true;
            }
        }

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

        IDatagramDecoder<string, byte[]> ITunnelCodec<string, byte[]>.Decoder
        {
            get { return StringDecoder; }
            set
            {
                StringDecoder = (KnifeStringDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<string, byte[]> ITunnelCodec<string, byte[]>.Encoder
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