using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.App.SocketKit.Common
{
    public class SocketTools
    {
        public Type Encoder { get; set; }
        public Type Decoder { get; set; }
        public Type Packer { get; set; }
        public Type UnPacker { get; set; }
        public Type CommandParser { get; set; }

        public bool NeedHeartBeat { get; set; }
    }
}
