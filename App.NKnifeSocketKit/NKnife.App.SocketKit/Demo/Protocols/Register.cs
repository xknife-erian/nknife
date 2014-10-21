using Ninject;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Demo.Protocols
{
    class Register : KnifeSocketProtocol
    {
        public Register() 
            : base("socket-kit", "register")
        {
        }

        [Inject]
        public override KnifeSocketProtocolPacker SocketPacker { get; set; }

        [Inject]
        public override KnifeSocketProtocolUnPacker SocketUnPacker { get; set; }
    }
}
