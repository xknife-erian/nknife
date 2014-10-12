using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.SocketKit.Dialogs;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.IoC
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISocketServerKnife>().To<Server>();
            Bind<ServerList>().ToSelf().InSingletonScope();
        }
    }
}
