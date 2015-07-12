using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
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
            Bind<BytesProtocolPacker>().To<NangleProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<NangleProtocolUnPacker>().InSingletonScope();
            Bind<BytesProtocolCommandParser>().To<NangleCommandParser>().InSingletonScope();

            Bind<BytesDatagramDecoder>().To<NangleDatagramDecoder>();
            Bind<BytesDatagramEncoder>().To<NangleDatagramEncoder>();
        }
    }
}
