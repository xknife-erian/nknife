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
        public override StringProtocolPacker Packer { get; set; }

        [Inject]
        public override StringProtocolUnPacker UnPacker { get; set; }
    }
}
