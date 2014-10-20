using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    public class FirstFieldCommandParser : KnifeSocketCommandParser
    {
        public override string GetCommand(string datagram)
        {
            var index = datagram.IndexOf("|", StringComparison.Ordinal);
            if (index > 0)
                return datagram.Substring(0, index);
            return string.Empty;
        }
    }
}
