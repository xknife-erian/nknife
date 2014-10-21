using Ninject;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Demo.Protocols
{
    class Register : StringProtocol
    {
        public Register() 
            : base("socket-kit", "register")
        {
        }

        [Inject]
        public override StringProtocolPacker SocketPacker { get; set; }

        [Inject]
        public override StringProtocolUnPacker SocketUnPacker { get; set; }
    }
}
