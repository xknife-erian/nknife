using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NKnife.Events;
using NLog;

namespace NKnife.Serials
{
    /// <summary>
    ///     串口集线器(多个串口的管理装置)
    /// </summary>
    public class SerialHub
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<ushort, (SerialConnector, SerialConfig)> _serialMap = new Dictionary<ushort, (SerialConnector, SerialConfig)>();

        /// <summary>
        ///     是否打印发送日志
        /// </summary>
        public bool IsPrintSendLogger { get; set; } = true;

        /// <summary>
        ///     是否打印接收日志
        /// </summary>
        public bool IsPrintReceivedLogger { get; set; } = true;

        public bool TryGetConfig(ushort currentPort, out SerialConfig serialConfig)
        {
            if (_serialMap.TryGetValue(currentPort, out var tuple))
            {
                serialConfig = tuple.Item2;
                return true;
            }

            serialConfig = null;
            return false;
        }

        public SerialConnector Index(ushort port)
        {
            return _serialMap.ContainsKey(port) ? _serialMap[port].Item1 : null;
        }

        public void Insert(ushort port, SerialConfig config)
        {
            if (!_serialMap.TryGetValue(port, out var tuple))
            {
                var serial = new SerialConnector {IntervalBetweenReadings = 200};
                serial.SetPort($"COM{port}", config);
                tuple = new Tuple<SerialConnector, SerialConfig>(serial, config).ToValueTuple();
                _serialMap.Add(port, tuple);
                _Logger.Info($"串口选择：COM{port}");
            }
            else
            {
                tuple.Item1.SetPort($"COM{port}", config);
                _Logger.Info($"串口选择：COM{port}");
            }

            var connector = tuple.Item1;
            connector.ConnectionStatusChanged += (s, e) => { _Logger.Info($"Connected = {e.Connected}"); };
            connector.MessageReceived += (s, e) =>
            {
                if (IsPrintReceivedLogger && _Logger.IsDebugEnabled)
                {
                    if (e.Data.IsASCII())
                        _Logger.Debug($"Rece: {Encoding.ASCII.GetString(e.Data)}");
                    else
                        _Logger.Debug($"Rece: {e.Data.ToHexString(" ")}");
                }
            };
        }

        public void RemoveAll()
        {
            foreach (var serial in _serialMap)
                serial.Value.Item1.Disconnect();
            _serialMap.Clear();
        }

        public void Remove(ushort port)
        {
            var serial = _serialMap[port].Item1;
            serial.Disconnect();
            _serialMap.Remove(port);
        }

        public void Start(ushort port)
        {
            _serialMap[port].Item1.Connect();
        }

        public void Stop(ushort port)
        {
            _serialMap[port].Item1.Disconnect();
        }

        public async Task SendCommand(ushort port, string command, int delay = 0, bool autoAppendReturnChar = true)
        {
            await Task.Factory.StartNew(() =>
            {
                if (!_serialMap.ContainsKey(port))
                    return;
                var serial = _serialMap[port].Item1;
                if (autoAppendReturnChar)
                    command = $"{command}\r";
                serial.SendMessage(command);
                var t = (port, command);
                OnDataSend(new EventArgs<(ushort, string)>(t));
                if (IsPrintSendLogger)
                    _Logger.Debug($"Sent:{command}");
                if (delay > 0)
                    Thread.Sleep(delay);
            });
        }

        public async Task SendByteArray(ushort port, byte[] data, int delay = 0)
        {
            await Task.Factory.StartNew(() =>
            {
                var serial = _serialMap[port].Item1;
                serial.SendMessage(data);
                var msg = data.ToHexString(" ");
                var t = (port, msg);
                OnDataSend(new EventArgs<(ushort, string)>(t));
                if (IsPrintSendLogger)
                    _Logger.Debug($"Sent:{msg}");
                if (delay > 0)
                    Thread.Sleep(delay);
            });
        }

        /// <summary>
        ///     当数据发送完成以后
        /// </summary>
        public event EventHandler<EventArgs<(ushort, string)>> DataSend;

        protected virtual void OnDataSend(EventArgs<(ushort, string)> e)
        {
            DataSend?.Invoke(this, e);
        }
    }
}