using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Consoles.Common;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using SerialKnife.Generic.Filters;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Consoles.Demos
{
    public class SerialClient
    {
        private const string FAMILY_NAME = "care-usb";
        private static readonly ILog _logger = LogManager.GetLogger<SerialClient>();
        private readonly ISerialConnector _DataConnector;
        private readonly ITunnel _Tunnel = DI.Get<ITunnel>();

        public SerialClient(int port)
        {
            var codec = DI.Get<BytesCodec>();
            codec.CodecName = "careone";
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;

            var queryFilter = DI.Get<QueryBusFilter>();
            queryFilter.Bind(codec, family);

            var handler = new SerialProtocolHandler();
            var protocolFilter = new SerialProtocolFilter();
            protocolFilter.Bind(codec, family);
            protocolFilter.AddHandlers(handler);

            _Tunnel.AddFilters(protocolFilter);

            _DataConnector = DI.Get<ISerialConnector>();
            _DataConnector.PortNumber = port; //串口

            _Tunnel.BindDataConnector(_DataConnector); //dataConnector是数据流动的动力
        }

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

        public void Send(byte[] data)
        {
            _DataConnector.SendAll(data);
        }

    }
}