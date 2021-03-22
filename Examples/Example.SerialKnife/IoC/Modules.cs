using Autofac;
using NKnife.Channels.SerialKnife.Services;
using NKnife.Channels.SerialKnife.Views;
using NKnife.Interface;

namespace NKnife.Channels.SerialKnife.IoC
{
    public class Modules : Module
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Workbench>().AsSelf().SingleInstance();
            builder.RegisterType<Global.About>().As<IAbout>().SingleInstance();
            builder.RegisterType<SerialChannelService>().AsSelf().SingleInstance();
            builder.RegisterType<LoggerView>().AsSelf().SingleInstance();
            builder.RegisterType<SerialPortView>().AsSelf().SingleInstance();
        }
    }
}
