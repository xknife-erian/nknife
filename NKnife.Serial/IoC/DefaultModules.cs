using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.NSerial.Common;
using NKnife.NSerial.Interfaces;
using NKnife.NSerial.Wrappers;

namespace NKnife.NSerial.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerialPortWrapper>().To<SerialPortWrapperDotNet>().Named(SerialType.DotNet.ToString());
            Bind<ISerialPortWrapper>().To<SerialPortWrapperWinApi>().Named(SerialType.WinApi.ToString());
        }
    }
}
