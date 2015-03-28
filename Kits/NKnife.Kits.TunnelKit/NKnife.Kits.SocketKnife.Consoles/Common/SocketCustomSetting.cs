using System;
using System.Net;

namespace NKnife.Kits.SocketKnife.Consoles.Common
{
    internal class SocketCustomSetting 
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
