using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using SocketKnife.Generic;
using SocketKnife.Interfaces;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISocketServerConfig>().To<DefaultSocketServerConfig>();
            Bind<IProtocolFamily>().To<DefaultProtocolFamily>();
            Bind<IProtocolTools>().To<DefaultProtocolTools>();
            Bind<ISocketSessionMap>().To<DefaultSocketSessionMap>();
            Bind<ISocketPolicy>().To<DefaultSocketPolicy>();
        }
    }
}
