using System;

namespace NKnife.Protocol.Generic.CommandParsers
{
    public class FirstFieldCommandParser : StringProtocolCommandParser
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
