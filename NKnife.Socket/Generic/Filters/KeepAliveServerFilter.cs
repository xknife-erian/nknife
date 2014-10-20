using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using NKnife.Adapters;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Tunnel.Events;
using NKnife.Utility;
using SocketKnife.Common;

namespace SocketKnife.Generic.Filters
{
    public class KeepAliveServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true;

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected override void OnBoundGetter(Func<KnifeSocketProtocolFamily> familyGetter,
            Func<KnifeSocketProtocolHandler[]> handlerGetter, Func<KnifeSocketSessionMap> sessionMapGetter,
            Func<KnifeSocketCodec> codecGetter)
        {
            KnifeSocketSessionMap map = sessionMapGetter.Invoke();
            map.Removed += SessionMap_OnRemoved;
        }

        private void SessionMap_OnRemoved(object sender, EventArgs<EndPoint> e)
        {
            ClearByEndPoint(e.Item);
        }


        protected internal override void OnClientBroke(ConnectionBreakEventArgs<EndPoint> e)
        {
            base.OnClientBroke(e);
            ClearByEndPoint(e.EndPoint);
        }

        private void ClearByEndPoint(EndPoint endPoint)
        {
            DataMonitor dataMonitor;
            if (_DataMonitors.TryRemove(endPoint, out dataMonitor))
            {
                dataMonitor.IsMonitor = false;
                dataMonitor.ReceiveQueue.AutoResetEvent.Set();
                _logger.Trace(string.Format("客户端:{0}的数据监听器池循环开关被关闭并移除。{1}", endPoint, _DataMonitors.Count));
            }
            ReceiveQueue receiveQueue;
            if (_ReceiveQueueMap.TryRemove(endPoint, out receiveQueue))
            {
                receiveQueue.Clear();
                _logger.Trace(string.Format("客户端:{0}从ReceiveQueue池中被移除。{1}", endPoint, _ReceiveQueueMap.Count));
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            EndPoint endPoint = session.Source;
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

        private readonly ConcurrentDictionary<EndPoint, DataMonitor> _DataMonitors =
            new ConcurrentDictionary<EndPoint, DataMonitor>();

        private readonly ConcurrentDictionary<EndPoint, ReceiveQueue> _ReceiveQueueMap =
            new ConcurrentDictionary<EndPoint, ReceiveQueue>();

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
            var pair = (KeyValuePair<EndPoint, ReceiveQueue>) obj;
            _logger.Debug(() => string.Format("启动基于{0}的ReceiveQueue队列的监听。", pair.Key));
            ReceiveQueue receiveQueue = pair.Value;
            var undone = new byte[] {};
            DataMonitor dataMonitor;
            if (_DataMonitors.TryGetValue(pair.Key, out dataMonitor))
            {
                while (dataMonitor.IsMonitor)
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
                            _logger.Trace(
                                () => string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", length, srcLen, data.Length));
                            undone = new byte[] {};
                        }
                        int done;
                        DataDecoder(pair.Key, data, out done);
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
            }
            // 当接收队列停止监听时，移除该客户端数据队列
            bool isRemoved = _DataMonitors.TryRemove(pair.Key, out dataMonitor);
            if (isRemoved)
                _logger.Trace(() => string.Format("从数据队列池中移除该客户端{0}成功，{1}", pair.Key, _DataMonitors.Count));
        }

        protected virtual void DataDecoder(EndPoint endpoint, byte[] data, out int done)
        {
            KnifeSocketProtocolFamily family = _FamilyGetter.Invoke();
            KnifeSocketCodec codec = _CodecGetter.Invoke();
            string[] datagram = codec.SocketDecoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
                return;
            }
            IEnumerable<KnifeSocketProtocol> protocols = ProtocolParse(family, datagram);
            foreach (KnifeSocketProtocol protocol in protocols)
            {
                // 触发数据基础解析后发生的数据到达事件
                HandlerInvoke(endpoint, protocol);
            }
        }

        protected virtual IEnumerable<KnifeSocketProtocol> ProtocolParse(KnifeSocketProtocolFamily family,
            string[] datagram)
        {
            var protocols = new List<KnifeSocketProtocol>(datagram.Length);
            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg)) continue;
                string command = "";
                try
                {
                    command = _CodecGetter.Invoke().SocketCommandParser.GetCommand(dg);
                }
                catch (Exception e)
                {
                    _logger.Error(string.Format("命令字解析异常:{0},Data:{1}", e.Message, dg), e);
                    continue;
                }
                _logger.Trace(string.Format("Server.OnDataComeIn::命令字:{0},数据包:{1}", command, dg));
                KnifeSocketProtocol protocol = family.NewProtocol(command);
                try
                {
                    protocol.Parse(dg);
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议分装异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
                    continue;
                }
                protocols.Add(protocol);
            }
            return protocols;
        }

        /// <summary>
        ///     // 触发数据基础解析后发生的数据到达事件
        /// </summary>
        protected virtual void HandlerInvoke(EndPoint endpoint, KnifeSocketProtocol protocol)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap sessionMap = _SessionMapGetter.Invoke();
            KnifeSocketSession session;
            if (!sessionMap.TryGetValue(endpoint, out session))
            {
                _logger.Warn(string.Format("SessionMap中未找到指定的客户端:{0}", endpoint));
            }
            try
            {
                if (handlers == null || handlers.Length == 0)
                {
                    Debug.Fail(string.Format("Handler集合不应为空."));
                    return;
                }
                if (handlers.Length == 1)
                {
                    handlers[0].Recevied(session, protocol);
                }
                else
                {
                    foreach (KnifeSocketProtocolHandler handler in handlers)
                    {
                        if (handler.Commands.Contains(protocol.Command))
                            handler.Recevied(session, protocol);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("handler调用异常:{0}", e.Message), e);
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