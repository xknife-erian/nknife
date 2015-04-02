using System;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SerialKnife.Consoles.CareOne
{
    public class CareOneDatagramDecoder : FixedTailDecoder
    {
        private byte[] _NewTail = new byte[] {0x0A};

        public override byte[] Tail
        {
            get { return _NewTail; }
            set { _NewTail = value; }
        }
    }
}