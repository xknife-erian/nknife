using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using NKnife.Utility;

namespace NKnife.Tunnel.Filters
{
    public class ProtocolFilter<TData> : BaseTunnelFilter, ITunnelProtocolFilter<TData>
    {
        private static readonly ILog _logger = LogManager.GetLogger<ProtocolFilter<TData>>();
        protected List<ITunnelProtocolHandler<TData>> _Handlers = new List<ITunnelProtocolHandler<TData>>();
        protected ITunnelCodec<TData> _Codec;
        protected IProtocolFamily<TData> _Family;

        protected readonly ConcurrentDictionary<long, DataMonitor> _DataMonitors = new ConcurrentDictionary<long, DataMonitor>();

        protected virtual void InitializeDataMonitor(long id, DataMonitor dm)
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

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var id = (long)obj;
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
        protected virtual void HandlerInvoke(long id, IProtocol<TData> protocol)
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

        public override event EventHandler<SessionEventArgs> SendToSession;
        public override event EventHandler<SessionEventArgs> SendToAll;
        public override event EventHandler<SessionEventArgs> KillSession;

        protected virtual void OnSendToSession(object sender, SessionEventArgs e)
        {
            var handler = SendToSession;
            if (handler != null)
                handler(this, e);
        }
        protected virtual void OnSendToAll(object sender, SessionEventArgs e)
        {
            var handler = SendToAll;
            if (handler != null)
                handler(this, e);
        }
        protected virtual void OnKillSession(object sender, SessionEventArgs e)
        {
            var handler = KillSession;
            if (handler != null)
                handler(this, e);
        }

        public virtual void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily)
        {
            _Codec = codec;
            _logger.Info(string.Format("绑定Codec成功。{0},{1}", _Codec.Decoder.GetType().Name, _Codec.Encoder.GetType().Name));

            _Family = protocolFamily;
            _logger.Info(string.Format("协议族[{0}]绑定成功。", _Family.FamilyName));
        }

        public override void ProcessSessionBroken(long id)
        {
            DataMonitor monitor;
            if (_DataMonitors.TryGetValue(id, out monitor))
            {
                monitor.IsMonitor = false;
                monitor.ReceiveQueue.AutoResetEvent.Set();
            }
        }

        public override void ProcessSessionBuilt(long id)
        {
            DataMonitor monitor;
            if (!_DataMonitors.TryGetValue(id, out monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
            //什么也不做
        }

        public override void ProcessSendToAll(byte[] data)
        {
            //什么也不做
        }

        public override bool PrcoessReceiveData(ITunnelSession session)
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

        public virtual void AddHandlers(params ITunnelProtocolHandler<TData>[] handlers)
        {
            foreach (var handler in handlers)
            {
                var phandler = handler as BaseProtocolHandler<TData>;
                if (phandler != null)
                {
                    phandler.SendToSession += OnSendToSession;
                    phandler.SendToAll += OnSendToAll;
                    phandler.Bind(_Codec, _Family);
                    _Handlers.Add(handler);
                }
            }
        }

        public virtual void RemoveHandler(ITunnelProtocolHandler<TData> handler)
        {
            handler.SendToSession -= OnSendToSession;
            handler.SendToAll -= OnSendToAll;
            _Handlers.Remove(handler);
        }

        /// <summary>
        /// 数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        public virtual IEnumerable<IProtocol<TData>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished)
        {
            if (!UtilityCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", unFinished.Length, srcLen, dataPacket.Length));
            }

            int done;
            TData[] datagram = _Codec.Decoder.Execute(dataPacket, out done);

            IEnumerable<IProtocol<TData>> protocols = null;

            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
            }
            else
            {
                protocols = ParseProtocols(datagram);
            }

            if (done!= 0 && dataPacket.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                unFinished = new byte[dataPacket.Length - done];
                Buffer.BlockCopy(dataPacket, done, unFinished, 0, unFinished.Length);
                _logger.Trace(string.Format("半包数据暂存,数据长度:{0}", unFinished.Length));
            }

            return protocols;
        }

        protected virtual IEnumerable<IProtocol<TData>> ParseProtocols(TData[] datagram)
        {
            var protocols = new List<IProtocol<TData>>(datagram.Length);
            foreach (TData dg in datagram)
            {
                TData command;
                try
                {
                    command = _Family.CommandParser.GetCommand(dg);
                }
                catch (Exception e)
                {
                    _logger.Error(string.Format("命令字解析异常:{0},Data:{1}", e.Message, dg), e);
                    continue;
                }
                _logger.Trace(string.Format("开始协议解析::命令字:{0},数据包:{1}", command, dg));

                IProtocol<TData> protocol;
                try
                {
                    protocol = _Family.Parse(command, dg);
                }
                catch (ArgumentNullException ex)
                {
                    _logger.Warn(string.Format("协议分装异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议分装异常。{0}", ex.Message), ex);
                    continue;
                }
                protocols.Add(protocol);
            }
            return protocols;
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