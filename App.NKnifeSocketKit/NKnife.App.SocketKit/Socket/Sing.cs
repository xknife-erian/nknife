using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.Socket
{
    class Sing : KnifeSocketProtocol
    {
        public Sing() 
            : base("socket-kit", "sing")
        {
        }
    }
}
