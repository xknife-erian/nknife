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
        [Named("xml")]
        public override KnifeSocketProtocolPackager SocketPackager { get; set; }

        [Inject]
        [Named("dataTable-deserialize")]
        public override KnifeSocketProtocolUnPackager SocketUnPackager { get; set; }
    }
}
