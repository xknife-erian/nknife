using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class StringCodec : ITunnelCodec<string>
    {
        private static readonly ILog _Logger = LogManager.GetLogger<StringCodec>();
        private bool _hasSetDecoder;
        private bool _hasSetEncoder;
        private StringDatagramDecoder _stringDecoder;
        private StringDatagramEncoder _stringEncoder;

        public StringCodec()
        {
        }

        public StringCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        public virtual StringDatagramDecoder StringDecoder
        {
            get
            {
                if (!_hasSetDecoder)
                {
                    _stringDecoder = string.IsNullOrEmpty(CodecName) ? Di.Get<StringDatagramDecoder>() : Di.Get<StringDatagramDecoder>(CodecName);
                    _hasSetDecoder = true;
                    return _stringDecoder;
                }
                return _stringDecoder;
            }
            set
            {
                _stringDecoder = value;
                _hasSetDecoder = true;
            }
        }

        public virtual StringDatagramEncoder StringEncoder
        {
            get
            {
                if (!_hasSetEncoder)
                {
                    _stringEncoder = string.IsNullOrEmpty(CodecName) ? Di.Get<StringDatagramEncoder>() : Di.Get<StringDatagramEncoder>(CodecName);
                    _hasSetEncoder = true;
                    return _stringEncoder;
                }
                return _stringEncoder;
            }
            set
            {
                _stringEncoder = value;
                _hasSetEncoder = true;
            }
        }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return StringDecoder; }
            set
            {
                StringDecoder = (StringDatagramDecoder) value;
                _Logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return StringEncoder; }
            set
            {
                StringEncoder = (StringDatagramEncoder) value;
                _Logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}