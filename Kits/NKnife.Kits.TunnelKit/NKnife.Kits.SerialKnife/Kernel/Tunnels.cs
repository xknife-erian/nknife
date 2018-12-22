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
using SerialKnife.Generic.Filters;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Kernel
{
    public class Tunnels
    {
        private const string FamilyName = "p-an485";
        private static readonly ILog _Logger = LogManager.GetLogger<Tunnels>();
        private readonly ISerialConnector _dataConnector;
        private readonly ITunnel _tunnel = Di.Get<ITunnel>();

        public Tunnels()
        {
            var logFilter = Di.Get<SerialLogFilter>();
            var queryFilter = Di.Get<QueryBusFilter>();
            var protocolFilter = Di.Get<SerialProtocolSimpleFilter>();
            var codec = Di.Get<BytesCodec>();
            var family = Di.Get<BytesProtocolFamily>();
            family.FamilyName = FamilyName;
            queryFilter.Bind(codec, family);
            protocolFilter.Bind(codec, family);
            protocolFilter.ProtocolsReceived += protocolFilter_ProtocolsReceived;

            _tunnel.AddFilters(logFilter);
            _tunnel.AddFilters(queryFilter);
            _tunnel.AddFilters(protocolFilter);

            _dataConnector = Di.Get<ISerialConnector>(Settings.Default.EnableMock ? "Mock" : "Serial");
            _dataConnector.PortNumber = Settings.Default.PortNumber; //串口1

            _tunnel.BindDataConnector(_dataConnector); //dataConnector是数据流动的动力
        }

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        public bool Start()
        {
            _dataConnector.Start();
            _Logger.Info("Tunnel服务启动成功");
            return true;
        }

        public bool Stop()
        {
            _dataConnector.Stop();
            _Logger.Info("Tunnel服务停止成功");
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