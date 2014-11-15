using Ninject.Modules;
using NKnife.App.Cute.Implement.Environment;

namespace NKnife.App.Cute.Kernel.IoC
{
    public class SingletonModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<Datas.Datas>().ToSelf().InSingletonScope();
            Bind<ActivityPool>().ToSelf().InSingletonScope();
            Bind<IdentifierGeneratorPool>().ToSelf().InSingletonScope();
            Bind<ServiceQueuePool>().ToSelf().InSingletonScope();
            Bind<UserPool>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
