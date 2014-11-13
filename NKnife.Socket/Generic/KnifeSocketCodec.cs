using Ninject;
using Common.Logging;
using NKnife.Interface;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public class KnifeSocketCodec : ITunnelCodec<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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
