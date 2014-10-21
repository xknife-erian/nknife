using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;
using SocketKnife.Generic.Protocols;

namespace NKnife.App.SocketKit.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerList>().ToSelf().InSingletonScope();

            Bind<KnifeSocketProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<KnifeSocketProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();

            Bind<KnifeSocketCommandParser>().To<FirstFieldCommandParser>().InSingletonScope();
            Bind<KnifeSocketDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeSocketDatagramEncoder>().To<FixedTailEncoder>();
        }
    }
}