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
    public class KeepAliveServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected internal override void OnConnectionBreak(ConnectionBreakEventArgs e)
        {
            base.OnConnectionBreak(e);
            var endPoint = e.EndPoint;
            if (endPoint != null && _DataMonitors.ContainsKey(endPoint))
            {
                var dataMonitor = _DataMonitors[endPoint];
                dataMonitor.IsMonitor = false;
                dataMonitor.ReceiveQueue.AutoResetEvent.Set();
                _logger.Trace(string.Format("Server: IP地址:{0}的数据池循环开关被关闭。{1}", endPoint, _DataMonitors.Count));
            }
        }

        public override void PrcoessReceiveData(Socket socket, byte[] data)
        {
            var endPoint = socket.RemoteEndPoint;
            ReceiveQueue receive = null;
            if (!_ReceiveQueueMap.TryGetValue(endPoint, out receive))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
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
            var thread = new Thread(ReceiveQueueMonitor) {IsBackground = true};
            var dm = new DataMonitor {IsMonitor = true, ReceiveQueue = pair.Value, Thread = thread};
            _DataMonitors.TryAdd(pair.Key, dm);
            thread.Start(pair);
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
            // 当接收队列停止监听时，移除该客户端数据队列
            DataMonitor outBool;
            bool remove = _DataMonitors.TryRemove(pair.Key, out outBool);
            if (remove)
                _logger.Trace(() => string.Format("从数据队列池中移除该客户端{0}成功，{1}", pair.Key, _DataMonitors.Count));
            else
                _logger.Warn(() => string.Format("从数据队列池中移除该客户端{0}不成功{1}", pair.Key, _DataMonitors.Count));
        }

        protected virtual void DataProcessBase(EndPoint endpoint, byte[] data, out int done)
        {
            var family = _FamilyGetter.Invoke();
            string[] datagram = family.Decoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
                return;
            }

            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg))
                    continue;
                string command = string.Empty;
                try
                {
                    command = family.CommandParser.GetCommand(dg);
                    IProtocol protocol = family.NewProtocol(command);
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
                    _logger.Warn(string.Format("预处理异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
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
