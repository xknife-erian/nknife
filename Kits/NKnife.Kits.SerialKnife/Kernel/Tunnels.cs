using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Filters;
using NKnife.Tunnel;
using SerialKnife.Interfaces;
using SerialKnife.Tunnel.Filters;

namespace NKnife.Kits.SerialKnife.Kernel
{
    public class Tunnels
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly ITunnel<byte[], int> _Tunnel = DI.Get<ITunnel<byte[], int>>();
        private readonly IKnifeSerialConnector _DataConnector = DI.Get<IKnifeSerialConnector>(); 

        public bool Start()
        {
            var logFilter = DI.Get<SerialLogFilter>();
            var queryFilter = DI.Get<QueryBusFilter>();

            _Tunnel.AddFilters(logFilter);
            _Tunnel.AddFilters(queryFilter);

            _DataConnector.PortNumber = 1; //串口1

            _Tunnel.BindDataConnector(_DataConnector); //dataConnector是数据流动的动力
            _Tunnel.Start();

            _logger.Info("Tunnel服务启动成功");
            return true;
        }

        public bool Stop()
        {
            _logger.Info("Tunnel服务停止成功");
            return true;
        }
    }
}
