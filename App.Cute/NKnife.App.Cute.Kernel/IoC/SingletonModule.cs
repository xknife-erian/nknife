using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Didaku.Engine.Timeaxis.Data;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Implement.Industry.Bank;
using Ninject.Modules;

namespace Didaku.Engine.Timeaxis.Kernel.IoC
{
    public class SingletonModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<Datas>().ToSelf().InSingletonScope();
            Bind<ActivityPool>().ToSelf().InSingletonScope();
            Bind<IdentifierGeneratorPool>().ToSelf().InSingletonScope();
            Bind<ServiceQueuePool>().ToSelf().InSingletonScope();
            Bind<UserPool>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
