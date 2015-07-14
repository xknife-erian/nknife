using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Generic;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Kits.SocketKnife.StressTest.View;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using SerialKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.StressTest.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            //逻辑
            Bind<TestKernel>().ToSelf().InSingletonScope();

            //通讯协议相关
            Bind<ITunnel>().To<KnifeTunnel>().Named("Server");
            Bind<ITunnel>().To<KnifeTunnel>().Named("Client");
            Bind<BytesProtocolPacker>().To<NangleProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<NangleProtocolUnPacker>().InSingletonScope();
            Bind<BytesProtocolCommandParser>().To<NangleCommandParser>().InSingletonScope();

            Bind<BytesDatagramDecoder>().To<NangleDatagramDecoder>();
            Bind<BytesDatagramEncoder>().To<NangleDatagramEncoder>();
            Bind<SerialLogFilter>().To<SerialLogFilter>();

            //界面相关
            Bind<LogView>().ToSelf().InSingletonScope();
            Bind<ServerView>().ToSelf().InSingletonScope();
            Bind<MockClientView>().ToSelf().InSingletonScope();
            Bind<ProtocolView>().ToSelf().InSingletonScope();

        }
    }
}
