using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.App.SocketKit.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<ObservableCollection<LayoutDocument>>().To<MessageDocumentCollection>().InSingletonScope();
        }
    }
}
