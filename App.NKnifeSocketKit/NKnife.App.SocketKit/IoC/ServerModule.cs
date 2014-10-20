using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Activation;
using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Demo;
using NKnife.App.SocketKit.Dialogs;
using NKnife.Protocol;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;
using SocketKnife.Generic.Protocols;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.IoC
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerList>().ToSelf().InSingletonScope();
            Bind<KnifeSocketServerBase>().To<KnifeSocketServer>();

            Bind<KnifeSocketProtocolPackager>().To<TextPlainPackager>().InSingletonScope();
            Bind<KnifeSocketProtocolUnPackager>().To<TextPlainUnPackager>().InSingletonScope();

            Bind<KnifeSocketCommandParser>().To<FirstFieldCommandParser>().InSingletonScope();
            Bind<KnifeSocketDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeSocketDatagramEncoder>().To<FixedTailEncoder>();
        }

        private bool IsMyServer(IRequest arg)
        {
            return true;
        }
    }


}
