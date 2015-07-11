using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    public class NangleProtocolPacker:BytesProtocolPacker
    {
        public override byte[] Combine(BytesProtocol content)
        {
            throw new NotImplementedException();
        }
    }
}
