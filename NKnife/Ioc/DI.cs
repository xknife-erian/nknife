using System.Reflection;
using Ninject;

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
            Assembly calling = Assembly.GetCallingAssembly();
            Assembly curr = Assembly.GetExecutingAssembly();
            _CoreKernel.Load(calling, curr);
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