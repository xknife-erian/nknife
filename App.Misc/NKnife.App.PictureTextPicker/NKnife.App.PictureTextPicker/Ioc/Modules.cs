using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.PictureTextPicker.Common;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Interface;
using NKnife.Wrapper;

namespace NKnife.App.PictureTextPicker.Ioc
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IAbout>().To<About>().InSingletonScope();
            Bind<IPictureList>().To<PictureList>().InSingletonScope();
        }
    }
}
