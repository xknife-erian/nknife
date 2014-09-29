using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using SocketKnife.Default;
using SocketKnife.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISocketServerConfig>().To<DefaultSocketServerConfig>();
        }
    }
}
