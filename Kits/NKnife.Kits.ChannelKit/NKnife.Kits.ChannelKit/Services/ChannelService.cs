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

        private readonly Dictionary<ushort, SerialConfig> _configMap = new Dictionary<ushort, SerialConfig>();
        private readonly Dictionary<SerialConfig, SerialChannel> _channelMap = new Dictionary<SerialConfig, SerialChannel>();

        public SerialChannel GetChannel(SerialConfig config)
        {
            if (!_channelMap.TryGetValue(config, out var channel))
            {
                channel = new SerialChannel(config);
                _channelMap.Add(config, channel);
            }
            return channel;
        }

        public SerialChannel GetChannel(ushort port)
        {
            var config = GetConfig(port);
            return GetChannel(config);
        }

        public void RemoveChannel(SerialConfig config)
        {
            if (_channelMap.TryGetValue(config, out var channel))
            {
                if (channel.IsOpen)
                    channel.Close();
                _configMap.Remove(config.Port);
                _channelMap.Remove(config);
            }
        }

        public void RemoveChannel(ushort port)
        {
            RemoveChannel(GetConfig(port));
        }

        public SerialConfig GetConfig(ushort port)
        {
            if (!_configMap.TryGetValue(port, out var config))
            {
                config = new SerialConfig(port);
                _configMap.Add(port, config);
            }
            return config;
        }

        #region Implementation of IDisposable

        /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
        public void Dispose()
        {
            foreach (var channel in _channelMap.Values)
            {
                if (channel.IsOpen)
                    channel.Close();
            }
            _channelMap.Clear();
            _configMap.Clear();
        }

        #endregion
    }
}