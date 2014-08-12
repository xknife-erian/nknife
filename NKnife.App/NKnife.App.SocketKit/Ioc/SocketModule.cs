using Ninject.Modules;
using NKnife.Socket.StarterKit.Base;
using SocketKnife.Config;

namespace NKnife.Socket.StarterKit.Ioc
{
    public class SocketModule : NinjectModule 
    {
        public override void Load()
        {
            Bind<SocketOption>().ToSelf().InSingletonScope();
            Bind<ProtocolSetting>().To<SocketProtocolSetting>().InSingletonScope();
        }
    }
}
