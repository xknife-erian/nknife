using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.SocketClient.StarterKit.Base;
using SocketKnife.Config;

namespace NKnife.SocketClient.StarterKit.Ioc
{
    public class SocketModule : NinjectModule 
    {
        public override void Load()
        {
            Bind<ProtocolSetting>().To<SocketProtocolSetting>();
        }
    }
}
