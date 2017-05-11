using Ninject.Modules;
using NKnife.Interface;
using NKnife.Kits.WinFormUIKit.Forms;

namespace NKnife.Kits.WinFormUIKit.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IAbout>().To<LibraryDemoWorkbench.CurrentAbout>().InSingletonScope();

            Bind<ChineseCharUseFrequencyDockView>().ToSelf().InSingletonScope();
            Bind<ImagesPanelControlTestDockView>().ToSelf().InSingletonScope();
            Bind<CustomStripControlTestDockView>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
