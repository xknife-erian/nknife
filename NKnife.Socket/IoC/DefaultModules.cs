using System.Net;
using System.Net.Sockets;
using Ninject.Modules;
using NKnife.Protocol.Generic;
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

            Bind<StringProtocol>().To<StringProtocol>();
            Bind<StringProtocolFamily>().To<StringProtocolFamily>();
            Bind<StringProtocolContent>().To<StringProtocolContent>();
        }
    }
}