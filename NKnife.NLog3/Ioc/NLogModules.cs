using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Adapters;

namespace NKnife.NLog3.Ioc
{
    public class NLogModules:NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggerFactory>().To<NLogLoggerFactory>().InSingletonScope();
        }
    }
}
