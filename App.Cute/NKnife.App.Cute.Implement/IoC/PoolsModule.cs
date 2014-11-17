using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.Cute.Implement.Environment;

namespace NKnife.App.Cute.Implement.IoC
{
    public class PoolsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UserPool>().ToSelf().InSingletonScope();
        }
    }
}
