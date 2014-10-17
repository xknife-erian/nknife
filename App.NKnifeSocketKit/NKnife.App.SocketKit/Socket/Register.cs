using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.Socket
{
    class Register : KnifeSocketProtocol
    {
        public Register() 
            : base("socket-kit", "register")
        {
        }
    }
}
