using Ninject.Activation;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;

namespace NKnife.IoC
{
    public class ConfiguringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserApplicationData>().To<UserApplicationData>().When(Request).InSingletonScope();
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
