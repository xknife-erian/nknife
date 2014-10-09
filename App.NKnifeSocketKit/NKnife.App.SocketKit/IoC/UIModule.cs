using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.App.SocketKit.IoC
{
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DockUtil>().ToSelf().InSingletonScope();
            Bind<ObservableCollection<LayoutContent>>().To<ViewCollection>().InSingletonScope();
        }
    }
}
