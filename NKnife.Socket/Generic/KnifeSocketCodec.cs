using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketCodec : ITunnelCodec<string>
    {
        [Inject]
        public virtual KnifeSocketDatagramDecoder SocketDecoder { get; set; }

        [Inject]
        public virtual KnifeSocketDatagramEncoder SocketEncoder { get; set; }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return SocketDecoder; }
            set { SocketDecoder = (KnifeSocketDatagramDecoder) value; }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return SocketEncoder; }
            set { SocketEncoder = (KnifeSocketDatagramEncoder) value; }
        }
    }
}
