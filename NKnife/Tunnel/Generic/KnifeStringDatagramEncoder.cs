using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Generic
{
    public abstract class KnifeStringDatagramEncoder : BaseDatagramEncoder<string>
    {
        public abstract override byte[] Execute(string replay);
    }
}