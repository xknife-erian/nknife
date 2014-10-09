using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput;
using Ninject.Modules;
using NKnife.App.TouchKnife.Common;
using NKnife.App.TouchKnife.Core;
using NKnife.App.TouchKnife.Ime.HandWritten;
using NKnife.App.TouchKnife.Ime.Pinyin;
using NKnife.App.TouchKnife.Workbench;
using NKnife.Interface;
using NKnife.Wrapper;

namespace NKnife.App.TouchKnife.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IAbout>().To<About>().InSingletonScope();

            Bind<Notify>().To<Notify>().InSingletonScope();
            Bind<Kernel>().To<Kernel>().InSingletonScope();

            Bind<InputSimulator>().To<InputSimulator>().InSingletonScope();
            Bind<Params>().To<Params>().InSingletonScope();
            Bind<HwAlternateCollection>().To<HwAlternateCollection>().InSingletonScope();
            Bind<PinyinSeparatesCollection>().To<PinyinSeparatesCollection>().InSingletonScope();
            Bind<PinyinAlternateCollection>().To<PinyinAlternateCollection>().InSingletonScope();
            Bind<CurrentWordStrip>().To<CurrentWordStrip>().InSingletonScope();
            Bind<ICharactorRecognizer>().To<ConcatenationRecognizer>().InSingletonScope();
            Bind<ISyllableCollection>().To<DefaultSyllableCollection>().InSingletonScope();
            Bind<IPinyinSeparator>().To<PinyinSeparator>().InSingletonScope();
        }
    }
}
