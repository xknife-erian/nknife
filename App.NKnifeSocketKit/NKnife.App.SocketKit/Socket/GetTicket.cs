using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.Socket
{
    class GetTicket : KnifeProtocol
    {
        public GetTicket() 
            : base("socket-kit", "get-ticket")
        {
        }
    }
}
