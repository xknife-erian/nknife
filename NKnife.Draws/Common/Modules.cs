using Ninject.Modules;

namespace NKnife.Draws.Common
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<Redraw>().To<Redraw>().InSingletonScope();
            Bind<RectangleList>().To<RectangleList>().InSingletonScope();
        }
    }
}
