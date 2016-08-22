using Ninject.Activation;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SocketKnife.Consoles.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>().When(Request);

            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();
            Bind<StringProtocolCommandParser>().To<TextPlainFirstFieldCommandParser>().InSingletonScope();

            Bind<StringDatagramDecoder>().To<FixedTailDecoder>();
            Bind<StringDatagramEncoder>().To<FixedTailEncoder>();
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}