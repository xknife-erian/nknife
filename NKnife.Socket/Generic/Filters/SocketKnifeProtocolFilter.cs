using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;

namespace SocketKnife.Generic.Filters
{
    public class SocketKnifeProtocolFilter : KnifeProtocolProcessorBase<string>, ITunnelFilter<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected List<KnifeProtocolHandlerBase<byte[], EndPoint, string>> Handlers = new List<KnifeProtocolHandlerBase<byte[], EndPoint, string>>();

        public event EventHandler<SessionEventArgs<byte[], EndPoint>> OnSendToSession;
        public event EventHandler<EventArgs<byte[]>> OnSendToAll;
        public event EventHandler<EventArgs<EndPoint>> OnKillSession;

        public SocketKnifeProtocolFilter()
        {

        }

        public void ProcessSessionBroken(EndPoint id)
        {
            DataMonitor monitor;
            if (_DataMonitors.TryGetValue(id, out monitor))
            {
                monitor.IsMonitor = false;
                monitor.ReceiveQueue.AutoResetEvent.Set();
            }
        }

        public void ProcessSessionBuilt(EndPoint id)
        {
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(id, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
        }

        public void ProcessSendToSession(ITunnelSession<byte[], EndPoint> session)
        {
            //什么也不做
        }

        public void ProcessSendToAll(byte[] data)
        {
            //什么也不做
        }



        public bool PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            var data = session.Data;
            EndPoint endPoint = session.Id;
            //ReceiveQueue receive;
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(endPoint, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(endPoint,monitor);
            }

            monitor.ReceiveQueue.Enqueue(data);
            return true;
        }

        public virtual void PrcoessSendData(ITunnelSession<byte[], EndPoint> session)
        {
            //默认啥也不干
        }


        /// <summary>
        ///     // 触发数据基础解析后发生的数据到达事件
        /// </summary>
        public virtual void HandlerInvoke(EndPoint endpoint, IProtocol<string> protocol)
        {
            try
            {
                if (Handlers.Count == 0)
                {
                    Debug.Fail(string.Format("Handler集合不应为空."));
                    return;
                }
                if (Handlers.Count == 1)
                {
                    Handlers[0].Recevied(endpoint, protocol);
                }
                else
                {
                    foreach (var handler in Handlers)
                    {
                        if (handler.Commands.Contains(protocol.Command))
                            handler.Recevied(endpoint, protocol);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("handler调用异常:{0}", e.Message), e);
            }
        }

        #region 数据处理
        private readonly ConcurrentDictionary<EndPoint, DataMonitor> _DataMonitors =
    new ConcurrentDictionary<EndPoint, DataMonitor>();

        public void AddHandlers(params KnifeProtocolHandlerBase<byte[], EndPoint, string>[] handlers)
        {
            foreach (var handler in handlers)
            {
                handler.OnSendToSession += handler_OnSendToSession;
                handler.OnSendToAll += handler_OnSendToAll;
                handler.Bind(Codec,Family);
            }
            Handlers.AddRange(handlers);
        }



        public void RemoveHandler(KnifeProtocolHandlerBase<byte[], EndPoint, string> handler)
        {
            handler.OnSendToSession -= handler_OnSendToSession;
            handler.OnSendToAll -= handler_OnSendToAll;
            Handlers.Remove(handler);
        }

        private void handler_OnSendToAll(object sender, EventArgs<byte[]> e)
        {
            var handler = OnSendToAll;
            if(handler !=null)
                handler.Invoke(this,e);
        }

        private void handler_OnSendToSession(object sender, SessionEventArgs<byte[], EndPoint> e)
        {
            var handler = OnSendToSession;
            if(handler !=null)
                handler.Invoke(this,e);
        }

        protected virtual void InitializeDataMonitor(EndPoint id,DataMonitor dm)
        {
            var task = new Task(ReceiveQueueMonitor, id);
            dm.IsMonitor = true;
            dm.Task = task;
            dm.ReceiveQueue = new ReceiveQueue();
            _DataMonitors.TryAdd(id, dm);
            task.Start();
        }

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var endPoint = (EndPoint)obj;
            DataMonitor dataMonitor;
            _logger.Debug(string.Format("启动基于{0}的ReceiveQueue队列的监听。", endPoint));
            var unFinished = new byte[] { };

            try
            {
                if (_DataMonitors.TryGetValue(endPoint, out dataMonitor))
                {
                    while (dataMonitor.IsMonitor || dataMonitor.ReceiveQueue.Count > 0) //重要，dataMonitor.IsMonitor=false但dataMonitor.ReceiveQueue.Count > 0时，也要继续处理完数据再退出while
                    {

                        if (dataMonitor.ReceiveQueue.Count > 0)
                        {
                            byte[] data = dataMonitor.ReceiveQueue.Dequeue();
                            var protocols = ProcessDataPacket(data, unFinished);

                            if (protocols != null)
                            {
                                foreach (var protocol in protocols)
                                {
                                    // 触发数据基础解析后发生的数据到达事件
                                    HandlerInvoke(endPoint, protocol);
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
                bool isRemoved = _DataMonitors.TryRemove(endPoint, out dataMonitor);
                if (isRemoved)
                    _logger.Trace(string.Format("监听循环结束，从数据队列池中移除该客户端{0}成功，{1}", endPoint, _DataMonitors.Count));
            }
        }




        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Task Task { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }
        #endregion
    }
}
