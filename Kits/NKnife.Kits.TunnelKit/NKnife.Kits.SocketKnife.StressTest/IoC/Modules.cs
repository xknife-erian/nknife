using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>().Named("Server");
            Bind<ITunnel>().To<KnifeTunnel>().Named("Client");
            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();
            Bind<StringProtocolCommandParser>().To<TextPlainFirstFieldCommandParser>().InSingletonScope();

            Bind<StringDatagramDecoder>().To<FixedTailDecoder>();
            Bind<StringDatagramEncoder>().To<FixedTailEncoder>();
        }
    }
}
