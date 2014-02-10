using System;
using Ninject;
using NKnife.Utility.File;

namespace NKnife.Ioc
{
    public static class DI
    {
        private static CoreKernel _CoreKernel;

        public static T Get<T>()
        {
            return _CoreKernel.Get<T>();
        }

        public static T Get<T>(string name)
        {
            return _CoreKernel.Get<T>(name);
        }

        public static void Initialize()
        {
            _CoreKernel = new CoreKernel();
            var assems = UtilityFile.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _CoreKernel.Load(assems);
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