using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketCommandParser : IDatagramCommandParser<string>
    {
        public abstract string GetCommand(string datagram);
    }
}
