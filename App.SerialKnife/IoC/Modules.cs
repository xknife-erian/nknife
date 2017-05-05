using Ninject.Modules;
using NKnife.Channels.SerialKnife.Services;
using NKnife.Interface;
using NKnife.Wrapper;

namespace NKnife.Channels.SerialKnife.IoC
{
    public class Modules : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IAbout>().To<Global.About>().InSingletonScope();
            Bind<SerialChannelService>().ToSelf().InSingletonScope();
        }
    }
}
