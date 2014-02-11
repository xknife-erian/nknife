using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Logging.Base;
using NKnife.Logging.LogPanel;

namespace NKnife.Ioc
{
    public class LoggingModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<LogPanel>().To<LogPanel>().InSingletonScope();
            Bind<LogDetailForm>().To<LogDetailForm>().InSingletonScope();
        }
    }
}
