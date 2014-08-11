using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Configuring.CoderSetting;
using NKnife.Interface;

namespace NKnife.Ioc
{
    public class ConfiguringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CoderSettingService>().To<CoderSettingService>().InSingletonScope();
        }
    }
}
