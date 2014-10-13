using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Protocol;

namespace NKnife.App.SocketKit.Socket
{
    class GetTicket : AbstractProtocol
    {
        public GetTicket() 
            : base("socket-kit", "get-ticket")
        {
        }
    }
}
