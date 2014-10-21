﻿namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolCommandParser : IProtocolCommandParser<string>
    {
        public abstract string GetCommand(string datagram);
    }
}
