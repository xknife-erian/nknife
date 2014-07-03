using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput;
using NKnife.Ioc;
using NKnife.TouchInput.Common;
using NKnife.TouchInput.Common.Recognize;
using NKnife.TouchInput.Xaml;

namespace NKnife.TouchInput.Ioc
{
    public class Module : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (AlternateCollection)).To(typeof (AlternateCollection)).InSingletonScope();
            Bind(typeof (TouchInputPanelParams)).To(typeof (TouchInputPanelParams)).InSingletonScope();
            Bind(typeof (InputCharCollection)).To(typeof (InputCharCollection)).InSingletonScope();
            Bind(typeof (CurrentWordStrip)).To(typeof (CurrentWordStrip)).InSingletonScope();
            Bind(typeof (InputSimulator)).To(typeof (InputSimulator)).InSingletonScope();
            Bind<ICharactorRecognizer>().To<ImprovedRecognizer>().InSingletonScope();
        }
    }
}
