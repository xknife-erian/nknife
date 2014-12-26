using Ninject.Modules;
using NKnife.NSerial.Common;
using NKnife.NSerial.Interfaces;
using NKnife.NSerial.Wrappers;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using SerialKnife.Tunnel.Tools;

namespace SerialKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel<byte[], int>>().To<KnifeTunnel<int>>(); //数据类型byte[], key类型int,表示串口号
            Bind<ISerialPortWrapper>().To<SerialPortWrapperDotNet>().Named(SerialType.DotNet.ToString());
            Bind<ISerialPortWrapper>().To<SerialPortWrapperWinApi>().Named(SerialType.WinApi.ToString());

            Bind<KnifeBytesDatagramDecoder>().To<FixByteHeadTailDatagramDecoder>().InSingletonScope();
            Bind<KnifeBytesDatagramEncoder>().To<FixByteHeadTailDatagramEncoder>().InSingletonScope();
            //tunnel protocol相关
            Bind<BytesProtocolCommandParser>().To<FirstByteCommandParser>().InSingletonScope();
            Bind<BytesProtocolPacker>().To<SimpleBytesProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<SimpleBytesProtocolUnPacker>().InSingletonScope();
        }
    }
}
