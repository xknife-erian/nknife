using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class KeepAliveServerFilter : KnifeSocketFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        public override void PrcoessReceiveData(Socket socket, byte[] data)
        {
            var endPoint = socket.RemoteEndPoint;

            if (!_DataMonitors.ContainsKey(endPoint))
            {
                var dataMonitor = new DataMonitor();
                _DataMonitors.AddOrUpdate(endPoint, dataMonitor, (p, m) => m);
                dataMonitor.IsMonitor = false;
                dataMonitor.ReceiveQueue.AutoResetEvent.Set();
                _logger.Trace(string.Format("Server: IP地址:{0}的数据池循环开关被关闭。{1}", endPoint, _DataMonitors.Count));
            }

            ReceiveQueue receive = null;
            if (!_ReceiveQueueMap.TryGetValue(endPoint, out receive))
            {
                receive = new ReceiveQueue();
                _ReceiveQueueMap.TryAdd(endPoint, receive);
                InitializeDataMonitor(new KeyValuePair<EndPoint, ReceiveQueue>(endPoint, receive));
            }
            receive.Enqueue(data);
        }

        #region 数据处理

        private readonly ConcurrentDictionary<EndPoint, DataMonitor> _DataMonitors = new ConcurrentDictionary<EndPoint, DataMonitor>();
        private readonly ConcurrentDictionary<EndPoint, ReceiveQueue> _ReceiveQueueMap = new ConcurrentDictionary<EndPoint, ReceiveQueue>();

        protected virtual void InitializeDataMonitor(KeyValuePair<EndPoint, ReceiveQueue> pair)
        {
            var t = new Thread(ReceiveQueueMonitor);
            var dm = new DataMonitor { IsMonitor = true, ReceiveQueue = pair.Value, Thread = t };
            _DataMonitors.TryAdd(pair.Key, dm);
            t.Start(pair);
        }

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var pair = (KeyValuePair<EndPoint, ReceiveQueue>)obj;
            _logger.Debug(() => string.Format("启动基于{0}的ReceiveQueue队列的监听。", pair.Key));
            ReceiveQueue receiveQueue = pair.Value;
            var undone = new byte[] { };
            while (_DataMonitors[pair.Key].IsMonitor)
            {
                if (receiveQueue.Count > 0)
                {
                    byte[] data = receiveQueue.Dequeue();
                    if (!UtilityCollection.IsNullOrEmpty(undone))
                    {
                        // 当有半包数据时，进行接包操作
                        int srcLen = data.Length;
                        var list = new List<byte>(data.Length + undone.Length);
                        list.AddRange(undone);
                        list.AddRange(data);
                        data = list.ToArray();
                        int length = undone.Length;
                        _logger.Trace(() => string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", length, srcLen, data.Length));
                        undone = new byte[] { };
                    }
                    int done;
                    DataProcessBase(pair.Key, data, out done);
                    if (data.Length > done)
                    {
                        // 暂存半包数据，留待下条队列数据接包使用
                        undone = new byte[data.Length - done];
                        Buffer.BlockCopy(data, done, undone, 0, undone.Length);
                        int length = undone.Length;
                        _logger.Trace(() => string.Format("半包数据暂存,数据长度:{0}", length));
                    }
                }
                else
                {
                    receiveQueue.AutoResetEvent.WaitOne();
                }
            }
            // 当接收队列完成后，移除该客户端信息
            DataMonitor outBool;
            bool remove = _DataMonitors.TryRemove(pair.Key, out outBool);
            if (remove)
                _logger.Trace(() => string.Format("接收队列完成，移除该客户端{0}信息，{1}", pair.Key, _DataMonitors.Count));
            else
                _logger.Trace(() => string.Format("接收队列完成，移除该客户端{0}信息不成功{1}", pair.Key, _DataMonitors.Count));
        }

        protected virtual void DataProcessBase(EndPoint endpoint, byte[] data, out int done)
        {
            string[] datagram = Decoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
                return;
            }

            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg))
                    continue;
                try
                {
                    string command = CommandParser.GetCommand(dg);
                    var family = _FamilyGetter.Invoke();
                    IProtocol protocol = family[command];
                    _logger.Trace(string.Format("Server.OnDataComeIn::命令字:{0},数据包:{1}", command, dg));
                    if (protocol != null)
                    {
                        protocol.Parse(dg);
                        // 触发数据基础解析后发生的数据到达事件
                        var handler = _HandlerGetter.Invoke();
                        var sessionMap = _SessionMapGetter.Invoke();
                        handler.Recevied(sessionMap[endpoint], protocol);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议字符串预处理异常。{0}", dg), ex);
                }
            }
        }

        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Thread Thread { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }

        #endregion

    }
}
