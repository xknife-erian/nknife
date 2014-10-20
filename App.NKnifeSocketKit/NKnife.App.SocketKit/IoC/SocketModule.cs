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
            Bind<KnifeSocketServerBase>().To<KnifeSocketServer>();

            Bind<KnifeSocketProtocolPackager>().To<TextPlainPackager>().InSingletonScope();
            Bind<KnifeSocketProtocolPackager>().To<ProtocolXmlPackager>().InSingletonScope().Named("xml");
            Bind<KnifeSocketProtocolUnPackager>().To<TextPlainUnPackager>().InSingletonScope();
            Bind<KnifeSocketProtocolUnPackager>().To<ProtocolDataTableDeserializeUnPackager>().InSingletonScope().Named("dataTable-deserialize");

            Bind<KnifeSocketCommandParser>().To<FirstFieldCommandParser>().InSingletonScope();
            Bind<KnifeSocketDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeSocketDatagramEncoder>().To<FixedTailEncoder>();
        }
    }
}