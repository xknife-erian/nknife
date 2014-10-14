using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
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
            Bind<ISocketServerConfig>().To<KnifeSocketServerConfig>();
            Bind<ISocketSessionMap>().To<KnifeSocketSessionMap>();
            Bind<ISocketPolicy>().To<KnifeSocketPolicy>();
            Bind<ISocketSession>().To<KnifeSocketSession>();

            Bind<IProtocolFamily>().To<KnifeProtocolFamily>();
            Bind<IProtocolContent>().To<KnifeProtocolContent>();
        }
    }
}
