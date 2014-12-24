﻿using Ninject.Modules;
using NKnife.NSerial.Common;
using NKnife.NSerial.Interfaces;
using NKnife.NSerial.Wrappers;
using NKnife.Tunnel;

namespace SerialKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel<byte[], int>>().To<KnifeTunnel<byte[], int>>(); //数据类型byte[], key类型int,表示串口号
            Bind<ISerialPortWrapper>().To<SerialPortWrapperDotNet>().Named(SerialType.DotNet.ToString());
            Bind<ISerialPortWrapper>().To<SerialPortWrapperWinApi>().Named(SerialType.WinApi.ToString());
        }
    }
}
