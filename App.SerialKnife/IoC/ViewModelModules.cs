using Ninject.Modules;
using NKnife.Channels.SerialKnife.Views;

namespace NKnife.Channels.SerialKnife.IoC
{
    public class ViewModelModules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<WorkbenchViewModel>().ToSelf().InSingletonScope();
            Bind<SerialPortViewModel>().ToSelf();
        }

        #endregion
    }
}
