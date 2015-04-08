using MonitorKnife.Tunnels.Common;
using Ninject.Modules;
using NKnife.Kits.SerialKnife.Consoles.CareOne;
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
            Bind<BytesCodec>().To<CareOneCodec>();
            Bind<BytesDatagramDecoder>().To<CareOneDatagramDecoder>().InSingletonScope().Named("careone");
            Bind<BytesDatagramEncoder>().To<CareOneDatagramEncoder>().InSingletonScope().Named("careone");
        }
    }
}