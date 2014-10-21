﻿using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.Packers;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace NKnife.App.SocketKit.IoC
{
    public class SocketModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerList>().ToSelf().InSingletonScope();

            Bind<StringProtocolPacker>().To<TextPlainPacker>().InSingletonScope();
            Bind<StringProtocolUnPacker>().To<TextPlainUnPacker>().InSingletonScope();

            Bind<KnifeSocketCommandParser>().To<FirstFieldCommandParser>().InSingletonScope();
            Bind<KnifeSocketDatagramDecoder>().To<FixedTailDecoder>();
            Bind<KnifeSocketDatagramEncoder>().To<FixedTailEncoder>();
        }
    }
}