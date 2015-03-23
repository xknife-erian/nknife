using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class KnifeBytesCodec : ITunnelCodec<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeBytesCodec>();
        private KnifeBytesDatagramDecoder _BytesDecoder;
        private KnifeBytesDatagramEncoder _BytesEncoder;
        private bool _HasSetDecoder;
        private bool _HasSetEncoder;

        public KnifeBytesCodec()
        {
        }

        public KnifeBytesCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual KnifeBytesDatagramDecoder BytesDecoder
        {
            get
            {
                if (!_HasSetDecoder)
                {
                    var d = string.IsNullOrEmpty(CodecName)
                        ? DI.Get<KnifeBytesDatagramDecoder>()
                        : DI.Get<KnifeBytesDatagramDecoder>(CodecName);
                    _HasSetDecoder = true;
                    return d;
                }
                return _BytesDecoder;
            }
            set
            {
                _BytesDecoder = value;
                _HasSetDecoder = true;
            }
        }

        public virtual KnifeBytesDatagramEncoder BytesEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                {
                    var e = string.IsNullOrEmpty(CodecName)
                        ? DI.Get<KnifeBytesDatagramEncoder>()
                        : DI.Get<KnifeBytesDatagramEncoder>(CodecName);
                    _HasSetEncoder = true;
                    return e;
                }
                return _BytesEncoder;
            }
            set
            {
                _BytesEncoder = value;
                _HasSetEncoder = true;
            }
        }

        IDatagramDecoder<byte[]> ITunnelCodec<byte[]>.Decoder
        {
            get { return BytesDecoder; }
            set
            {
                BytesDecoder = (KnifeBytesDatagramDecoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<byte[]> ITunnelCodec<byte[]>.Encoder
        {
            get { return BytesEncoder; }
            set
            {
                BytesEncoder = (KnifeBytesDatagramEncoder) value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}