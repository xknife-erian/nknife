using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using SocketKnife.Views;

namespace SocketKnife.IoC
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
