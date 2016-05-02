﻿using MonitorKnife.Tunnels.Common;
using Ninject.Activation;
using Ninject.Modules;
using NKnife.Kits.SerialKnife.Consoles.CareOne;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;
using SerialKnife;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Consoles.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerialConnector>().To<SerialPortDataConnector>();
            Bind<ITunnel>().To<KnifeTunnel>().Named("Server");

            Bind<BytesCodec>().To<CareOneCodec>();
            Bind<BytesProtocolCommandParser>().To<CareOneProtocolCommandParser>().InSingletonScope();
            Bind<BytesDatagramDecoder>().To<CareOneDatagramDecoder>().InSingletonScope().Named("careone");
            Bind<BytesDatagramEncoder>().To<CareOneDatagramEncoder>().InSingletonScope().Named("careone");
            Bind<BytesProtocol>().To<CareSaying>();

            Bind<BytesProtocolPacker>().To<CareOneProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<CareOneProtocolUnPacker>().InSingletonScope();
        }
    }
}