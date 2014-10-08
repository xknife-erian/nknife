﻿using Ninject.Modules;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.UserData;

namespace NKnife.IoCcvc
{
    public class ConfiguringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserApplicationData>().To<UserApplicationData>().InSingletonScope();
        }
    }
}
