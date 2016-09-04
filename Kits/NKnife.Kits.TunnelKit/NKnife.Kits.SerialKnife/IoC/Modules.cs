using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Kits.SerialKnife.Mock;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;
using SerialKnife;
using SerialKnife.Interfaces;
using SerialKnife.Pan.ProtocolTools;

namespace NKnife.Kits.SerialKnife.IoC
{
    public class Modules:NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>().Named("Server");
            Bind<ISerialConnector>().To<MockSerialDataConnector>().InSingletonScope().Named("Mock");
            Bind<ISerialConnector>().To<SerialPortDataConnector>().Named("Serial");

            Bind<BytesDatagramDecoder>().To<PanFixByteHeadTailDatagramDecoder>().InSingletonScope();
            Bind<BytesDatagramEncoder>().To<PanFixByteHeadTailDatagramEncoder>().InSingletonScope();
        }
    }
}
