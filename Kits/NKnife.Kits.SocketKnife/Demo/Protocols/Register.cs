using Ninject;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.Demo.Protocols
{
    class Register : StringProtocol
    {
        public Register() 
            : base("socket-kit", "register")
        {
        }

    }
}
