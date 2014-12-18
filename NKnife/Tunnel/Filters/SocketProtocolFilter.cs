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
    public class SocketProtocolFilter : ProtocolProcessorBase<string>, ITunnelFilter<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();


        IFilterListener<byte[], EndPoint> ITunnelFilter<byte[], EndPoint>.Listener
        {
            get { return Listener; }
            set { Listener = (SocketProtocolFilterListener) value; }
        }

        public SocketProtocolFilterListener Listener { get; set; }

        public bool ContinueNextFilter { get; private set; }


        public SocketProtocolFilter()
        {
            Listener = new SocketProtocolFilterListener();
        }

        public void ProcessSessionBroken(EndPoint id)
        {
        }

        public void ProcessSessionBuilt(EndPoint id)
        {

        }
        public void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            var data = session.Data;
            EndPoint endPoint = session.Id;
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

        public void AddHandlers(params KnifeProtocolHandlerBase<byte[], EndPoint, string>[] handlers)
        {
            Listener.AddHandlers(handlers);
        }

        public void RemoveHandler(KnifeProtocolHandlerBase<byte[], EndPoint, string> handler)
        {
            Listener.RemoveHandler(handler);
        }

        protected virtual void InitializeDataMonitor(KeyValuePair<EndPoint, ReceiveQueue> pair)
        {
            var task = new Task(ReceiveQueueMonitor, pair);
            var dm = new DataMonitor { IsMonitor = true, ReceiveQueue = pair.Value, Task = task };
            _DataMonitors.TryAdd(pair.Key, dm);
            task.Start();
        }

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var pair = (KeyValuePair<EndPoint, ReceiveQueue>)obj;
            _logger.Debug(string.Format("启动基于{0}的ReceiveQueue队列的监听。", pair.Key));
            ReceiveQueue receiveQueue = pair.Value;

            var unFinished = new byte[] { };
            DataMonitor dataMonitor;
            EndPoint endPoint = pair.Key;
            if (_DataMonitors.TryGetValue(endPoint, out dataMonitor))
            {
                while (dataMonitor.IsMonitor)
                {
                    if (receiveQueue.Count > 0)
                    {
                        byte[] data = receiveQueue.Dequeue();
                        var protocols = ProcessDataPacket(data, unFinished);

                        if (protocols != null)
                        {
                            foreach (var protocol in protocols)
                            {
                                // 触发数据基础解析后发生的数据到达事件
                                Listener.HandlerInvoke(endPoint, (StringProtocol) protocol);
                            }
                        }
                    }
                    else
                    {
                        receiveQueue.AutoResetEvent.WaitOne();
                    }
                }
            }
            // 当接收队列停止监听时，移除该客户端数据队列
            bool isRemoved = _DataMonitors.TryRemove(endPoint, out dataMonitor);
            if (isRemoved)
                _logger.Trace(string.Format("监听循环结束，从数据队列池中移除该客户端{0}成功，{1}", endPoint, _DataMonitors.Count));
        }




        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Task Task { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }

        public class SocketProtocolFilterListener : IFilterListener<byte[], EndPoint>
        {
            protected List<KnifeProtocolHandlerBase<byte[], EndPoint, string>> Handlers = new List<KnifeProtocolHandlerBase<byte[], EndPoint, string>>();

            public ISessionProvider<byte[], EndPoint> SessionProvider { get; private set; }

            #region IFilterListener接口
            public void OnSessionBroken(EndPoint id)
            {
                
            }

            public void OnSessionBuilt(EndPoint id)
            {
               
            }

            public void BindSessionHandler(ISessionProvider<byte[], EndPoint> sessionProvider)
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
            #endregion

            public virtual void AddHandlers(params KnifeProtocolHandlerBase<byte[], EndPoint, string>[] handlers)
            {
                Handlers.AddRange(handlers);
            }

            public virtual void RemoveHandler(KnifeProtocolHandlerBase<byte[], EndPoint, string> handler)
            {
                Handlers.Remove(handler);
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
        }
        #endregion
    }
}
