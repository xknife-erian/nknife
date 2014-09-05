using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Configuring;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.UserData;
using NKnife.Interface;

namespace NKnife.Ioc
{
    public class ConfiguringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserApplicationData>().To<UserApplicationData>().InSingletonScope();
        }
    }
}
