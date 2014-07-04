using WindowsInput;
using Ninject.Modules;
using NKnife.TouchInput.Common;
using NKnife.TouchInput.Common.PinyinIme;
using NKnife.TouchInput.Common.Recognize;
using NKnife.TouchInput.HandWrittenIme;
using NKnife.TouchInput.Xaml;

namespace NKnife.TouchInput.Ioc
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Kernal>().To<Kernal>().InSingletonScope();
            Bind<HwAlternateCollection>().To<HwAlternateCollection>().InSingletonScope();
            Bind<PinyinSeparatesCollection>().To<PinyinSeparatesCollection>().InSingletonScope();
            Bind<PyAlternatesStrip>().To<PyAlternatesStrip>().InSingletonScope();
            Bind<HwAlternatesStrip>().To<HwAlternatesStrip>().InSingletonScope();
            Bind<CurrentWordStrip>().To<CurrentWordStrip>().InSingletonScope();
            Bind<InputSimulator>().To<InputSimulator>().InSingletonScope();
            Bind<ICharactorRecognizer>().To<ConcatenationRecognizer>().InSingletonScope();
            Bind<ISyllableCollection>().To<DefaultSyllableCollection>().InSingletonScope();
            Bind<IPinyinSeparator>().To<PinyinSeparator>().InSingletonScope();
        }
    }
}