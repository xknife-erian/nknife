using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Activation;
using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.Mvvm.ViewModels;
using NKnife.Protocol;
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
            Bind<IKnifeSocketServer>().To<KnifeSocketServer>();

            Bind<IProtocolPackager>().To<TextPlainPackager>().InSingletonScope();
            Bind<IProtocolUnPackager>().To<TextPlainUnPackager>().InSingletonScope();

            Bind<ISocketSessionMap>().To<DemoSocketSessionMap>().When(IsMyServer);
        }

        private bool IsMyServer(IRequest arg)
        {
            return true;
        }
    }


}
