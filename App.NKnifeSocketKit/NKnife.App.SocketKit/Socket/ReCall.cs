using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.Socket
{
    class ReCall : KnifeSocketProtocol
    {
        public ReCall() 
            : base("socket-kit", "re-call")
        {
        }
    }
}
