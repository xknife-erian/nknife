using WindowsInput;
using Ninject.Modules;
using NKnife.Chinese.Ime.HandWritten;
using NKnife.Chinese.Ime.Pinyin;
using NKnife.Chinese.TouchInput;

namespace NKnife.Chinese
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<InputSimulator>().To<InputSimulator>().InSingletonScope();
            Bind<Kernal>().To<Kernal>().InSingletonScope();
            Bind<HwAlternateCollection>().To<HwAlternateCollection>().InSingletonScope();
            Bind<PinyinSeparatesCollection>().To<PinyinSeparatesCollection>().InSingletonScope();
            Bind<PyAlternatesStrip>().To<PyAlternatesStrip>().InSingletonScope();
            Bind<HwAlternatesStrip>().To<HwAlternatesStrip>().InSingletonScope();
            Bind<CurrentWordStrip>().To<CurrentWordStrip>().InSingletonScope();
            Bind<ICharactorRecognizer>().To<ConcatenationRecognizer>().InSingletonScope();
            Bind<ISyllableCollection>().To<DefaultSyllableCollection>().InSingletonScope();
            Bind<IPinyinSeparator>().To<PinyinSeparator>().InSingletonScope();
        }
    }
}