﻿using NKnife.Protocol.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneProtocolCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            return new[] {datagram[3], datagram[4]};
        }
    }
}