using WindowsInput;
using Ninject.Modules;
using NKnife.App.TouchInputKnife.Commons;
using NKnife.App.TouchInputKnife.Commons.HandWritten;
using NKnife.App.TouchInputKnife.Commons.Pinyin;
using NKnife.App.TouchInputKnife.Core;
using NKnife.App.TouchInputKnife.Workbench;
using NKnife.Configuring.Interfaces;
using NKnife.Interface;
using NKnife.Wrapper;

namespace NKnife.App.TouchInputKnife.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IAbout>().To<About>().InSingletonScope();

            Bind<IOptionManager>().To<Option>().InSingletonScope();
            Bind<Notify>().To<Notify>().InSingletonScope();
            Bind<Kernel>().To<Kernel>().InSingletonScope();

            Bind<ICharactorRecognizer>().To<ConcatenationRecognizer>().InSingletonScope();
            Bind<ISyllableCollection>().To<DefaultSyllableCollection>().InSingletonScope();
            Bind<IPinyinSeparator>().To<PinyinSeparator>().InSingletonScope();

            Bind<InputSimulator>().To<InputSimulator>().InSingletonScope();
            Bind<Params>().To<Params>().InSingletonScope();

            Bind<HwAlternateCollection>().To<HwAlternateCollection>().InSingletonScope();

            Bind<PinyinCollection>().To<PinyinCollection>().InSingletonScope();
            Bind<PinyinAlternateCollection>().To<PinyinAlternateCollection>().InSingletonScope();

            Bind<CurrentWordStrip>().To<CurrentWordStrip>().InSingletonScope();
        }
    }
}
