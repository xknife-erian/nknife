using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Filters;
using NKnife.Kits.SerialKnife.Properties;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using SerialKnife.Interfaces;
using SerialKnife.Tunnel.Filters;

namespace NKnife.Kits.SerialKnife.Kernel
{
    public class Tunnels
    {
        private const string FAMILY_NAME = "p-an485";
        private static readonly ILog _logger = LogManager.GetLogger<Tunnels>();
        private readonly IKnifeSerialConnector _DataConnector;
        private readonly ITunnel _Tunnel = DI.Get<ITunnel>();

        public Tunnels()
        {
            var logFilter = DI.Get<SerialLogFilter>();
            var queryFilter = DI.Get<QueryBusFilter>();
            var protocolFilter = DI.Get<SerialProtocolFilter>();
            var codec = DI.Get<KnifeBytesCodec>();
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;
            queryFilter.Bind(codec, family);
            protocolFilter.Bind(codec, family);
            protocolFilter.ProtocolsReceived += protocolFilter_ProtocolsReceived;

            _Tunnel.AddFilters(logFilter);
            _Tunnel.AddFilters(queryFilter);
            _Tunnel.AddFilters(protocolFilter);

            _DataConnector = DI.Get<IKnifeSerialConnector>(Settings.Default.EnableMock ? "Mock" : "Serial");
            _DataConnector.PortNumber = Settings.Default.PortNumber; //串口1

            _Tunnel.BindDataConnector(_DataConnector); //dataConnector是数据流动的动力
        }

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        public bool Start()
        {
            _DataConnector.Start();
            _logger.Info("Tunnel服务启动成功");
            return true;
        }

        public bool Stop()
        {
            _DataConnector.Stop();
            _logger.Info("Tunnel服务停止成功");
            return true;
        }

        /// <summary>
        ///     协议接收处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void protocolFilter_ProtocolsReceived(object sender, EventArgs<IEnumerable<IProtocol<byte[]>>> e)
        {
            EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> handler = ProtocolsReceived;
            if (handler != null)
                handler.Invoke(sender, e);
        }
    }
}