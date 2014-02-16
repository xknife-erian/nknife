using System;
using Ninject;
using Ninject.Modules;
using NKnife.Utility.File;

namespace NKnife.Ioc
{
    public static class DI
    {
        private static bool _IsInitialized;
        private static CoreKernel _CoreKernel;

        public static T Get<T>()
        {
            Initialize();
            return _CoreKernel.Get<T>();
        }

        public static T Get<T>(string name)
        {
            Initialize();
            return _CoreKernel.Get<T>(name);
        }

        public static void Initialize()
        {
            if (_IsInitialized) return;
            _CoreKernel = new CoreKernel();
            var assems = UtilityFile.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _CoreKernel.Load(assems);
            _IsInitialized = true;
        }

        public static void AddModule(NinjectModule module)
        {
            Initialize();
            _CoreKernel.Load(module);
        }

        private class CoreKernel : StandardKernel
        {
            public CoreKernel()
                : base(new NinjectSettings {InjectNonPublic = true})
            {
            }
        }


    }
}