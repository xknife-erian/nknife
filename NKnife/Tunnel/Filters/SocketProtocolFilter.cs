using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Generic;

namespace NKnife.Tunnel.Filters
{
    public class SocketProtocolFilter : ProtocolProcessorBase<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected List<KnifeProtocolHandlerBase<byte[], EndPoint, string>> Handlers = new List<KnifeProtocolHandlerBase<byte[], EndPoint, string>>();

        public ISessionProvider<byte[], EndPoint> SessionProvider { get; private set; }

        public SocketProtocolFilter()
        {
            ContinueNextFilter = true;
        }

        public override void BindSessionHandler(ISessionProvider<byte[], EndPoint> sessionProvider)
        {
            SessionProvider = sessionProvider;
            if (Handlers.Count == 0)
            {
                _logger.Warn("绑定SessionProvider时Handler集合不应为空，在此之前应先执行AddHandler动作.");
            }
            else
            {
                foreach (var handler in Handlers)
                {
                    handler.SessionProvider = sessionProvider;
                }
            }
        }

        public override void ProcessSessionBroken(EndPoint id)
        {
            DataMonitor monitor;
            if (_DataMonitors.TryGetValue(id, out monitor))
            {
                monitor.IsMonitor = false;
                monitor.ReceiveQueue.AutoResetEvent.Set();
            }

            _DataMonitors.TryRemove(id, out monitor);
        }

        public override void ProcessSessionBuilt(EndPoint id)
        {
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(id, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
        }
        public override void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
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
        }


        /// <summary>
        ///     // 触发数据基础解析后发生的数据到达事件
        /// </summary>
        public virtual void HandlerInvoke(EndPoint endpoint, IProtocol<string> protocol)
        {
            if (SessionProvider == null)
            {
                _logger.Warn("没有SessionProvider，无法处理协议");
                return;
            }

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
            Handlers.AddRange(handlers);
        }

        public void RemoveHandler(KnifeProtocolHandlerBase<byte[], EndPoint, string> handler)
        {
            Handlers.Remove(handler);
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
                    while (dataMonitor.IsMonitor)
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
