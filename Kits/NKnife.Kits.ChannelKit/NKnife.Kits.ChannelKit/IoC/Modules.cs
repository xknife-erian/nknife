using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using NKnife.Kits.ChannelKit.Services;
using NKnife.Kits.ChannelKit.ViewModels;

namespace NKnife.Kits.ChannelKit.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<ChannelService>().ToSelf().InSingletonScope();

            Bind<MainViewmodel>().ToSelf().InSingletonScope();
            Bind<SingleSerialViewmodel>().ToSelf();
        }

        #endregion
    }
}
