using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class BytesCodec : ITunnelCodec<byte[]>
    {
        private static readonly ILog _Logger = LogManager.GetLogger<BytesCodec>();
        private BytesDatagramDecoder _bytesDecoder;
        private BytesDatagramEncoder _bytesEncoder;
        private bool _hasSetDecoder;
        private bool _hasSetEncoder;

        public BytesCodec()
        {
        }

        public BytesCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual BytesDatagramDecoder BytesDecoder
        {
            get
            {
                if (!_hasSetDecoder)
                {
                    _bytesDecoder = string.IsNullOrEmpty(CodecName)
                        ? Di.Get<BytesDatagramDecoder>()
                        : Di.Get<BytesDatagramDecoder>(CodecName);
                    _hasSetDecoder = true;
                    return _bytesDecoder;
                }
                return _bytesDecoder;
            }
            set
            {
                _bytesDecoder = value;
                _hasSetDecoder = true;
            }
        }

        public virtual BytesDatagramEncoder BytesEncoder
        {
            get
            {
                if (!_hasSetEncoder)
                {
                    _bytesEncoder = string.IsNullOrEmpty(CodecName)
                        ? Di.Get<BytesDatagramEncoder>()
                        : Di.Get<BytesDatagramEncoder>(CodecName);
                    _hasSetEncoder = true;
                    return _bytesEncoder;
                }
                return _bytesEncoder;
            }
            set
            {
                _bytesEncoder = value;
                _hasSetEncoder = true;
            }
        }

        IDatagramDecoder<byte[]> ITunnelCodec<byte[]>.Decoder
        {
            get { return BytesDecoder; }
            set
            {
                BytesDecoder = (BytesDatagramDecoder) value;
                _Logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<byte[]> ITunnelCodec<byte[]>.Encoder
        {
            get { return BytesEncoder; }
            set
            {
                BytesEncoder = (BytesDatagramEncoder) value;
                _Logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}