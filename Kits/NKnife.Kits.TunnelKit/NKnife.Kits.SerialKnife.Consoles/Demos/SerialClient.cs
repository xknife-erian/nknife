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
using SerialKnife.Common;
using SerialKnife.Generic.Filters;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Consoles.Demos
{
    public class SerialClient
    {
        private const string FamilyName = "care-usb";
        private static readonly ILog _Logger = LogManager.GetLogger<SerialClient>();
        private readonly ISerialConnector _dataConnector;
        private readonly ITunnel _tunnel = Di.Get<ITunnel>("Server");

        public SerialClient(int port)
        {
            var codec = Di.Get<BytesCodec>();
            codec.CodecName = "careone";
            var family = Di.Get<BytesProtocolFamily>();
            family.FamilyName = FamilyName;

            var handler = new SerialProtocolHandler();
            var protocolFilter = new SerialProtocolFilter();
            protocolFilter.Bind(codec, family);
            protocolFilter.AddHandlers(handler);

            _tunnel.AddFilters(protocolFilter);

            _dataConnector = Di.Get<ISerialConnector>();
            _dataConnector.SerialType = SerialType.DotNet;
            _dataConnector.SerialConfig = new SerialConfig()
            {
                BaudRate = 115200,
                ReadBufferSize = 258,
                ReadTimeout = 100
            };
            _dataConnector.PortNumber = port; //串口

            _tunnel.BindDataConnector(_dataConnector); //dataConnector是数据流动的动力
        }

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

        public void Send(byte[] data)
        {
            _dataConnector.SendAll(data);
        }

    }
}