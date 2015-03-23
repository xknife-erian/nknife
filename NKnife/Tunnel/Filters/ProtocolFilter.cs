using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Filters
{
    public class ProtocolFilter<TSessionId> : ITunnelProtocolFilter<TSessionId, byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<ProtocolFilter<TSessionId>>();
        protected List<KnifeProtocolHandlerBase<TSessionId, byte[]>> _Handlers = new List<KnifeProtocolHandlerBase<TSessionId, byte[]>>();

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var id = (TSessionId)obj;
            DataMonitor dataMonitor;
            _logger.Debug(string.Format("启动基于{0}的ReceiveQueue队列的监听。", id));
            var unFinished = new byte[] {};

            try
            {
                if (_DataMonitors.TryGetValue(id, out dataMonitor))
                {
                    while (dataMonitor.IsMonitor || dataMonitor.ReceiveQueue.Count > 0) //重要，dataMonitor.IsMonitor=false但dataMonitor.ReceiveQueue.Count > 0时，也要继续处理完数据再退出while
                    {
                        if (dataMonitor.ReceiveQueue.Count > 0)
                        {
                            var data = dataMonitor.ReceiveQueue.Dequeue();
                            var protocols = ProcessDataPacket(data, unFinished);

                            if (protocols != null)
                            {
                                foreach (var protocol in protocols)
                                {
                                    // 触发数据基础解析后发生的数据到达事件, 即触发handle
                                    HandlerInvoke(id, protocol);
                                }
                            }
                        }
                        else
                        {
                            dataMonitor.ReceiveQueue.AutoResetEvent.WaitOne();
                        }
                    }
                }
                // 当接收队列停止监听时，移除该客户端数据队列
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("监听循环异常结束：{0}", ex));
            }
            finally
            {
                var isRemoved = _DataMonitors.TryRemove(id, out dataMonitor);
                if (isRemoved)
                {
                    _logger.Trace(string.Format("监听循环结束，从数据队列池中移除该客户端{0}成功，{1}", id, _DataMonitors.Count));
                }
            }
        }

        /// <summary>
        ///     触发数据基础解析后发生的数据到达事件
        /// </summary>
        protected virtual void HandlerInvoke(TSessionId id, IProtocol<byte[]> protocol)
        {
            try
            {
                if (_Handlers.Count == 0)
                {
                    Debug.Fail(string.Format("Handler集合不应为空."));
                    return;
                }
                if (_Handlers.Count == 1)
                {
                    _Handlers[0].Recevied(id, protocol);
                }
                else
                {
                    foreach (var handler in _Handlers)
                    {
                        if (handler.Commands.Contains(protocol.Command))
                        {
                            handler.Recevied(id, protocol);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("handler调用异常:{0}", e.Message), e);
            }
        }

        #region interface

        public event EventHandler<SessionEventArgs<byte[], TSessionId>> OnSendToSession;
        public event EventHandler<EventArgs<byte[]>> OnSendToAll;
        public event EventHandler<EventArgs<TSessionId>> OnKillSession;

        public virtual void ProcessSessionBroken(TSessionId id)
        {
            DataMonitor monitor;
            if (_DataMonitors.TryGetValue(id, out monitor))
            {
                monitor.IsMonitor = false;
                monitor.ReceiveQueue.AutoResetEvent.Set();
            }
        }

        public virtual void ProcessSessionBuilt(TSessionId id)
        {
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(id, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
        }

        public virtual void ProcessSendToSession(ITunnelSession<byte[], int> session)
        {
            //什么也不做
        }

        public virtual void ProcessSendToAll(byte[] data)
        {
            //什么也不做
        }

        public virtual bool PrcoessReceiveData(ITunnelSession<byte[], TSessionId> session)
        {
            var data = session.Data;
            var id = session.Id;
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(id, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }

            monitor.ReceiveQueue.Enqueue(data);
            return true;
        }

        #endregion

        #region 数据处理

        private readonly ConcurrentDictionary<TSessionId, DataMonitor> _DataMonitors = new ConcurrentDictionary<TSessionId, DataMonitor>();

        public virtual void AddHandlers(params KnifeProtocolHandlerBase<TSessionId, byte[]>[] handlers)
        {
            foreach (var handler in handlers)
            {
                handler.OnSendToSession += Handler_OnSendToSession;
                handler.OnSendToAll += Handler_OnSendToAll;
                handler.Bind(_Codec, _Family);
            }
            _Handlers.AddRange(handlers);
        }

        public virtual void RemoveHandler(KnifeProtocolHandlerBase<TSessionId, byte[]> handler)
        {
            handler.OnSendToSession -= Handler_OnSendToSession;
            handler.OnSendToAll -= Handler_OnSendToAll;
            _Handlers.Remove(handler);
        }

        protected virtual void Handler_OnSendToAll(object sender, EventArgs<byte[]> e)
        {
            var handler = OnSendToAll;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        protected virtual void Handler_OnSendToSession(object sender, SessionEventArgs<TSessionId> e)
        {
            var handler = OnSendToSession;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        protected virtual void InitializeDataMonitor(TSessionId id, DataMonitor dm)
        {
            var task = new Task(ReceiveQueueMonitor, id);
            dm.IsMonitor = true;
            dm.Task = task;
            dm.ReceiveQueue = new ReceiveQueue();
            if (_DataMonitors.TryAdd(id, dm))
            {
                dm.Task.Start();
            }
        }

        #endregion
        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Task Task { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }
    }
}