using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Kits.StarterKit.Forms;

namespace NKnife.Kits.StarterKit.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IAbout>().To<LibraryDemoWorkbench.CurrentAbout>().InSingletonScope();

            Bind<LoggingDockView>().ToSelf().InSingletonScope();
            Bind<ChineseCharUseFrequencyDockView>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
