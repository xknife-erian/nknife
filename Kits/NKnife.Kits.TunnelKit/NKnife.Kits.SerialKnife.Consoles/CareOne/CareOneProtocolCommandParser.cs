using System;
using NKnife.Protocol.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneProtocolCommandParser : StringProtocolCommandParser
    {
        public override string GetCommand(string datagram)
        {
            var ds = datagram.Split(new[] {'`'});
            return ds[0];
        }
    }
}