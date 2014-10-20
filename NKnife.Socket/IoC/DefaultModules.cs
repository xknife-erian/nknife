using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Protocol;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;
using SocketKnife.Generic.Protocols;
using SocketKnife.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<KnifeSocketServerConfig>().To<KnifeSocketServerConfig>();
            Bind<KnifeSocketFilterChain>().To<KnifeSocketFilterChain>();
            Bind<KnifeSocketSessionMap>().To<KnifeSocketSessionMap>();
            Bind<KnifeSocketSession>().To<KnifeSocketSession>();

            Bind<KnifeSocketProtocolFamily>().To<KnifeSocketProtocolFamily>();
            Bind<KnifeSocketProtocolContent>().To<KnifeSocketProtocolContent>();
        }
    }
}
