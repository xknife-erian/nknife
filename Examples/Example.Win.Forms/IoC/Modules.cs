using Autofac;
using NKnife.Win.Forms.Kit.Forms;
using NKnife.Interface;

namespace NKnife.Win.Forms.Kit.IoC
{
    public class Modules : Module
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CurrentAbout>().As<IAbout>().SingleInstance();
            builder.RegisterType<LibraryDemoWorkbench>().SingleInstance();
            builder.RegisterType<ImagesPanelControlTestDockView>().SingleInstance();
            builder.RegisterType<CustomStripControlTestDockView>().SingleInstance();
        }

        #endregion
    }
}
