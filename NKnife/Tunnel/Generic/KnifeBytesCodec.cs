using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.IoC;

namespace NKnife.Tunnel.Generic
{
    public class KnifeBytesCodec : ITunnelCodec<byte[],byte[]>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public KnifeBytesCodec()
        {

        }

        public KnifeBytesCodec(string codecName)
        {
            CodecName = codecName;
        }

        public string CodecName { get; set; }

        private KnifeBytesDatagramDecoder _BytesDecoder;
        private bool _HasSetDecoder;
        public virtual KnifeBytesDatagramDecoder BytesDecoder
        {
            get
            {
                if (!_HasSetDecoder)
                {
                    return string.IsNullOrEmpty(CodecName)
                        ? DI.Get<KnifeBytesDatagramDecoder>()
                        : DI.Get<KnifeBytesDatagramDecoder>(CodecName);
                }
                return _BytesDecoder;
            }
            set
            {
                _BytesDecoder = value;
                _HasSetDecoder = true;
            }
        }

        private KnifeBytesDatagramEncoder _BytesEncoder;
        private bool _HasSetEncoder;

        public virtual KnifeBytesDatagramEncoder BytesEncoder
        {
            get
            {
                if (!_HasSetEncoder)
                {
                    return string.IsNullOrEmpty(CodecName) ? DI.Get<KnifeBytesDatagramEncoder>() : DI.Get<KnifeBytesDatagramEncoder>(CodecName);
                }
                return _BytesEncoder;
            }
            set
            {
                _BytesEncoder = value;
                _HasSetEncoder = true;
            }
        }

        IDatagramDecoder<byte[], byte[]> ITunnelCodec<byte[], byte[]>.Decoder
        {
            get { return BytesDecoder; }
            set
            {
                BytesDecoder = (KnifeBytesDatagramDecoder)value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }

        IDatagramEncoder<byte[], byte[]> ITunnelCodec<byte[], byte[]>.Encoder
        {
            get { return BytesEncoder; }
            set
            {
                BytesEncoder = (KnifeBytesDatagramEncoder)value;
                _logger.Info(string.Format("{0}绑定成功。", value.GetType().Name));
            }
        }
    }
}
