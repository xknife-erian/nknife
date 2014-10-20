using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Demo
{
    public class DemoCodec : KnifeSocketCodec
    {
        public override ISocketCommandParser SocketCommandParser { get; set; }
        public override KnifeSocketDatagramDecoder SocketDecoder { get; set; }
        public override KnifeSocketDatagramEncoder SocketEncoder { get; set; }
    }
}
