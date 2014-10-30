using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public class KnifeSocketCodec : ITunnelCodec<string>
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        [Inject]
        public virtual KnifeSocketDatagramDecoder SocketDecoder { get; set; }

        [Inject]
        public virtual KnifeSocketDatagramEncoder SocketEncoder { get; set; }

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
