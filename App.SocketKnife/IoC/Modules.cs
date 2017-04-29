using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ninject.Modules;
using NKnife.Interface;
using SocketKnife.Services;

namespace SocketKnife.IoC
{
    public class Modules : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IAbout>().To<Global.About>().InSingletonScope();
            Bind<SerialChannelService>().ToSelf().InSingletonScope();
        }
    }
}
