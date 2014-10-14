using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.Socket
{
    class Register : KnifeProtocol
    {
        public Register() 
            : base("socket-kit", "register")
        {
        }
    }
}
