using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using SocketKnife;
using SocketKnife.Generic.Protocols;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.IoC
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerList>().ToSelf().InSingletonScope();
            Bind<IKnifeSocketServer>().To<KnifeServer>();

            Bind<IProtocolPackager>().To<TextPlainPackager>().InSingletonScope();
            Bind<IProtocolUnPackager>().To<TextPlainUnPackager>().InSingletonScope();
        }
    }
}
