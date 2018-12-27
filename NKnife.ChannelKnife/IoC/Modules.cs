using Ninject.Modules;
using NKnife.Interface;
using NKnife.Kits.ChannelKit.View;
using NKnife.Kits.ChannelKit.ViewModels;

namespace NKnife.Kits.ChannelKit.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <inheritdoc />
        public override void Load()
        {
            Bind<IAbout>().To<AboutMe>().InSingletonScope();
            Bind<Workbench>().ToSelf().InSingletonScope();

            Bind<WorkbenchViewModel>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}