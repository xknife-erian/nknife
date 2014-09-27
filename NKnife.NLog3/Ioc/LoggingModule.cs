using NKnife.NLog3.Logging.Base;
using NKnife.NLog3.Logging.LogPanel;

namespace NKnife.NLog3.Ioc
{
    public class LoggingModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<LogPanel>().To<LogPanel>().InSingletonScope();
            Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
        }
    }
}
