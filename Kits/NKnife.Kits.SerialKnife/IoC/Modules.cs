using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Kits.SerialKnife.Mock;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.IoC
{
    public class Modules:NinjectModule
    {
        public override void Load()
        {
            Bind<IKnifeSerialConnector>().To<MockSerialDataConnector>().InSingletonScope().Named("Mock");
        }
    }
}
