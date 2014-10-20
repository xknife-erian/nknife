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
        public override KnifeSocketProtocolPackager SocketPackager { get; set; }

        [Inject]
        public override KnifeSocketProtocolUnPackager SocketUnPackager { get; set; }
    }
}
