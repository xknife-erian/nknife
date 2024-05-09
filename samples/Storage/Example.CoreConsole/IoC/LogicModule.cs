using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Example.Logic;

namespace Example.CoreConsole.IoC
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookManagerLogic>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterType<PersonManagerLogic>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterType<BuyLogic>().AsImplementedInterfaces().AsSelf().SingleInstance();
        }
    }
}
