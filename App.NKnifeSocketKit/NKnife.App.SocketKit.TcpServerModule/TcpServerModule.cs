using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace NKnife.App.SocketKit.TcpServerModule
{
    class TcpServerModule : IModule
    {
        private readonly IRegionManager _RegionManager;

        public TcpServerModule(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
        }

        public void Initialize()
        {
            _RegionManager.RegisterViewWithRegion("TcpServer", typeof(MainView));
        }
    }
}
