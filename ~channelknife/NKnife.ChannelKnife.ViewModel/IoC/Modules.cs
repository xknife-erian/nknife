using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace NKnife.ChannelKnife.ViewModel.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <inheritdoc />
        public override void Load()
        {
            Bind<PortSelectorDialogViewModel>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
