using System.Net;
using System.Net.Sockets;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<SocketTunnel>().Named("Socket");

            Bind<IKnifeSocketServer>().To<KnifeSocketServer>();
            Bind<IKnifeSocketClient>().To<KnifeLongSocketClient>();

            Bind<KnifeSocketConfig>().To<KnifeSocketServerConfig>().Named("Server");
            Bind<KnifeSocketConfig>().To<KnifeSocketClientConfig>().Named("Client");

            Bind<KnifeSocketSessionMap>().To<KnifeSocketSessionMap>().When(Request);
            Bind<KnifeSocketSession>().To<KnifeSocketSession>().When(Request);

            Bind<StringProtocol>().To<StringProtocol>().When(Request);
            Bind<StringProtocolFamily>().To<StringProtocolFamily>().When(Request);
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}