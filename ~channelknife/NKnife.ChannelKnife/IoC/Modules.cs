using Ninject.Modules;
using NKnife.ChannelKnife.View;
using NKnife.ChannelKnife.ViewModels;
using NKnife.Interface;

namespace NKnife.ChannelKnife.IoC
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