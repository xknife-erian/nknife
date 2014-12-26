using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Filters;
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
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private const string FAMILY_NAME = "p-an485";
        private readonly ITunnel<byte[], int> _Tunnel = DI.Get<ITunnel<byte[], int>>();
        private readonly IKnifeSerialConnector _DataConnector = DI.Get<IKnifeSerialConnector>("Mock");
        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        public Tunnels()
        {
            var logFilter = DI.Get<SerialLogFilter>();
            var queryFilter = DI.Get<QueryBusFilter>();
            var protocolFilter = DI.Get<ProtocolHandleFilter>();
            var codec = DI.Get<KnifeBytesCodec>();
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;
            queryFilter.Bind(codec,family);
            protocolFilter.Bind(codec,family);
            protocolFilter.ProtocolsReceived += protocolFilter_ProtocolsReceived;

            _Tunnel.AddFilters(logFilter);
            _Tunnel.AddFilters(queryFilter);
            _Tunnel.AddFilters(protocolFilter);

            _DataConnector.PortNumber = 1; //串口1

            _Tunnel.BindDataConnector(_DataConnector); //dataConnector是数据流动的动力
        }

        public bool Start()
        {
            _Tunnel.Start();
            _logger.Info("Tunnel服务启动成功");
            return true;
        }

        public bool Stop()
        {
            _Tunnel.Stop();
            _logger.Info("Tunnel服务停止成功");
            return true;
        }

        /// <summary>
        /// 协议接收处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void protocolFilter_ProtocolsReceived(object sender, Events.EventArgs<IEnumerable<Protocol.IProtocol<byte[]>>> e)
        {
            var handler = ProtocolsReceived;
            if(handler !=null)
                handler.Invoke(sender,e);
        }
    }
}
