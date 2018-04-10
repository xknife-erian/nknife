using System;
using System.Collections.Generic;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Services
{
    public class ChannelService: IDisposable
    {
        public void Initialize()
        {
            SerialUtils.RefreshSerialPorts();
        }

        private readonly Dictionary<ushort, SerialConfig> _ConfigMap = new Dictionary<ushort, SerialConfig>();
        private readonly Dictionary<SerialConfig, SerialChannel> _ChannelMap = new Dictionary<SerialConfig, SerialChannel>();

        public SerialChannel GetChannel(SerialConfig config)
        {
            if (!_ChannelMap.TryGetValue(config, out var channel))
            {
                channel = new SerialChannel(config);
                _ConfigMap.Add(config.Port, config);
                _ChannelMap.Add(config, channel);
            }
            return channel;
        }

        public SerialChannel GetChannel(ushort port)
        {
            return _ChannelMap[_ConfigMap[port]];
        }

        public void DisposeChannel(SerialConfig config)
        {
            if (_ChannelMap.TryGetValue(config, out var channel))
            {
                if (channel.IsOpen)
                    channel.Close();
                _ConfigMap.Remove(config.Port);
                _ChannelMap.Remove(config);
            }
        }

        public SerialConfig GetConfig(ushort port)
        {
            if (!_ConfigMap.TryGetValue(port, out var config))
            {
                config = new SerialConfig(port);
                _ConfigMap.Add(port, config);
            }
            return config;
        }

        #region Implementation of IDisposable

        /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
        public void Dispose()
        {
            foreach (var channel in _ChannelMap.Values)
            {
                if (channel.IsOpen)
                    channel.Close();
            }
            _ChannelMap.Clear();
            _ConfigMap.Clear();
        }

        #endregion
    }
}