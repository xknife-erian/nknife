using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;

namespace NKnife.Jsons.Protocol
{
    public class JsonDecoder : IDatagramDecoder<string>
    {
        public string[] Execute(byte[] data, out int finishedIndex)
        {
            throw new NotImplementedException();
        }
    }
}
