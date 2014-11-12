using Ninject.Modules;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace NKnife.Kits.SocketKnife.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerMap>().ToSelf().InSingletonScope();

            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();
            Bind<StringProtocolCommandParser>().To<TextPlainFirstFieldCommandParser>().InSingletonScope();

            Bind<KnifeSocketDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeSocketDatagramEncoder>().To<FixedTailEncoder>();

            Bind<StringProtocol>().To<MyProtocol>().Named("TestCustom");
        }

        public class MyProtocol : StringProtocol
        {
        }
    }
}