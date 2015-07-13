using System;
using NKnife.Protocol;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class NangleProtocolEventArgs : EventArgs
    {
        public long SessionId { get; set; }
        public BytesProtocol Protocol { get; set; }
    }
}