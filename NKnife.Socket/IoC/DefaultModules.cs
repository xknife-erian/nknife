﻿using System.Net;
using System.Net.Sockets;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Planning.Bindings;
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

            Bind<KnifeSocketServerConfig>().To<KnifeSocketServerConfig>().When(Request);
            Bind<KnifeSocketSessionMap>().To<KnifeSocketSessionMap>().When(Request);
            Bind<KnifeSocketSession>().To<KnifeSocketSession>().When(Request);

            Bind<StringProtocol>().To<StringProtocol>().When(Request);
            Bind<StringProtocolFamily>().To<StringProtocolFamily>().When(Request);
            Bind<StringProtocolContent>().To<StringProtocolContent>().When(Request);
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}