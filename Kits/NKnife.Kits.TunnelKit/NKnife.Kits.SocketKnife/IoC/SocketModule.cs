using Ninject.Modules;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>();

            Bind<ServerMap>().ToSelf().InSingletonScope();

            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();
            Bind<StringProtocolCommandParser>().To<TextPlainFirstFieldCommandParser>().InSingletonScope();

            Bind<StringDatagramDecoder>().To<FixedTailDecoder>();
            Bind<StringDatagramEncoder>().To<FixedTailEncoder>();

            Bind<StringProtocol>().To<MyProtocol>().Named("TestCustom");
        }

        public class MyProtocol : StringProtocol
        {
        }
    }
}