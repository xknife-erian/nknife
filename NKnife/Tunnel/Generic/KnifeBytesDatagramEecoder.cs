using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Generic
{
    public abstract class KnifeBytesDatagramEncoder : IDatagramEncoder<byte[], byte[]>
    {
        public abstract byte[] Execute(byte[] data);
    }
}
