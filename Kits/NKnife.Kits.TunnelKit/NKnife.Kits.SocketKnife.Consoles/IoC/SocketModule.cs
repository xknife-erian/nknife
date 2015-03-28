using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel.Generic;
using SocketKnife.Generic.Families;

namespace NKnife.Kits.SocketKnife.Consoles.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();
            Bind<StringProtocolCommandParser>().To<TextPlainFirstFieldCommandParser>().InSingletonScope();

            Bind<KnifeStringDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeStringDatagramEncoder>().To<FixedTailEncoder>();
        }
    }
}