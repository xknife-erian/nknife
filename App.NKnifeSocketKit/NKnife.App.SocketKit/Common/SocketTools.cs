using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NKnife.App.SocketKit.Common
{
    public class SocketTools
    {
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }

        public Type Encoder { get; set; }
        public Type Decoder { get; set; }
        public Type Packer { get; set; }
        public Type UnPacker { get; set; }
        public Type CommandParser { get; set; }

        public bool NeedHeartBeat { get; set; }
    }
}
