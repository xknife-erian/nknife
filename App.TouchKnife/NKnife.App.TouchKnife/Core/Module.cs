using WindowsInput;
using Ninject.Modules;
using NKnife.App.TouchKnife.Common;
using NKnife.App.TouchKnife.Ime.HandWritten;
using NKnife.App.TouchKnife.Ime.Pinyin;

namespace NKnife.App.TouchIme.TouchInput.Common
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
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