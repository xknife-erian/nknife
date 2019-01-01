using Ninject.Modules;
using NKnife.ChannelKnife.ViewModel;
using NKnife.ChannelKnife.Views;
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
            Bind<Dialogs>().ToSelf().InSingletonScope();

            Bind<LoggerView>().ToSelf().InSingletonScope();

            Bind<WorkbenchViewModel>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}