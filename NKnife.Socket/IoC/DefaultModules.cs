using System.Net;
using System.Net.Sockets;
using Ninject.Modules;
using NKnife.Tunnel;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IKnifeSocketServer>().To<KnifeSocketServer>();

            Bind<ITunnelFilterChain<EndPoint, Socket>>().To<KnifeSocketServerFilterChain>().Named("Server");
            Bind<ITunnelFilterChain<EndPoint, Socket>>().To<KnifeSocketClientFilterChain>().Named("Client");

            Bind<KnifeSocketServerConfig>().To<KnifeSocketServerConfig>();
            Bind<KnifeSocketSessionMap>().To<KnifeSocketSessionMap>();
            Bind<KnifeSocketSession>().To<KnifeSocketSession>();

            Bind<KnifeSocketProtocol>().To<KnifeSocketProtocol>();
            Bind<KnifeSocketProtocolFamily>().To<KnifeSocketProtocolFamily>();
            Bind<KnifeSocketProtocolContent>().To<KnifeSocketProtocolContent>();
        }
    }
}