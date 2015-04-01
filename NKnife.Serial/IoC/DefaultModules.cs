using Ninject.Activation;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;
using SerialKnife.Common;
using SerialKnife.Generic.Tools;
using SerialKnife.Interfaces;
using SerialKnife.Wrappers;

namespace SerialKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>().When(Request); ; //数据类型byte[], key类型int,表示串口号
            Bind<ISerialPortWrapper>().To<SerialPortWrapperDotNet>().Named(SerialType.DotNet.ToString());
            Bind<ISerialPortWrapper>().To<SerialPortWrapperWinApi>().Named(SerialType.WinApi.ToString());

            Bind<KnifeBytesDatagramDecoder>().To<PanFixByteHeadTailDatagramDecoder>().InSingletonScope();
            Bind<KnifeBytesDatagramEncoder>().To<PanFixByteHeadTailDatagramEncoder>().InSingletonScope();

            //tunnel protocol相关
            Bind<BytesProtocolCommandParser>().To<PanFirstByteCommandParser>().InSingletonScope();
            Bind<BytesProtocolPacker>().To<SimpleBytesProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<SimpleBytesProtocolUnPacker>().InSingletonScope();
        }
        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
