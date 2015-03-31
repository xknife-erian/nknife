using Ninject.Modules;
using SerialKnife;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Consoles.IoC
{
    public class Modules:NinjectModule
    {
        public override void Load()
        {
            Bind<IKnifeSerialConnector>().To<SerialPortDataConnector>();
        }
    }
}
