﻿using System.Collections.ObjectModel;
using Ninject.Modules;
using NKnife.Kits.SocketKnife.Common;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.Kits.SocketKnife.IoC
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