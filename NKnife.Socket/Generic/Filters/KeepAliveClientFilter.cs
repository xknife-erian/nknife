﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using NKnife.Adapters;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Protocol.Generic;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class KeepAliveClientFilter : KnifeSocketClientFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();
        /// <summary>
        ///     是否启动接收队列的监听
        /// </summary>
        protected bool _EnableReceiveQueueMonitor = true;

        /// <summary>
        ///     接收的数据队列
        /// </summary>
        protected readonly ReceiveQueue _ReceiveQueue = new ReceiveQueue();
        protected bool _ContinueNextFilter = true;
        public override bool ContinueNextFilter { get { return _ContinueNextFilter; } }

        protected internal override void OnConnected(ConnectedEventArgs e)
        {
            base.OnConnected(e);
            //当连接启动后，启动数据池监听线程
            var thread = new Thread(ReceiveQueueMonitor);
            thread.Start();
        }

        protected internal override void OnConnectionBroken(ConnectionBrokenEventArgs e)
        {
            base.OnConnectionBroken(e);
            _EnableReceiveQueueMonitor = false; //停止监听
            _ReceiveQueue.AutoResetEvent.Set();

        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            _ReceiveQueue.Enqueue(data);
        }

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor()
        {
            _logger.Info("启动ReceiveQueue队列的监听。");
            var unFinished = new byte[] { };
            while (_EnableReceiveQueueMonitor)
            {
                if (_ReceiveQueue.Count > 0)
                {
                    byte[] data = _ReceiveQueue.Dequeue();
                    unFinished = ProcessDataPacket(data, unFinished, DataDecoder, SessionGetter.Invoke().Source);
                }
                else
                {
                    _ReceiveQueue.AutoResetEvent.WaitOne();
                }
            }
            _logger.Info("退出ReceiveQueue队列的监听。");
        }

        /// <summary>
        ///     处理协议数据
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        protected virtual int DataDecoder(EndPoint endpoint, byte[] data)
        {
            int finishedIndex;
            var codec = _CodecGetter.Invoke();
            string[] datagram = codec.SocketDecoder.Execute(data, out finishedIndex);
            OnDataDecoded(new SocketDataDecodedEventArgs(endpoint, datagram));
            if (UtilityCollection.IsNullOrEmpty(datagram))
                return finishedIndex;

            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg))
                    continue;
                string command = _FamilyGetter.Invoke().CommandParser.GetCommand(dg);
                StringProtocol protocol = _FamilyGetter.Invoke().Build(command);
                string dgByLog = dg;
                _logger.Trace(() => string.Format("From:命令字:{0},数据包:{1}", command, dgByLog));
                if (protocol != null)
                {
                    protocol.Parse(dg);
                    // 触发数据基础解析后发生的数据到达事件
                    HandlerInvoke(endpoint, protocol);
                }
            }
            return finishedIndex;
        }

        /// <summary>
        ///    触发数据基础解析后发生的数据到达事件
        /// </summary>
        protected virtual void HandlerInvoke(EndPoint endpoint, StringProtocol protocol)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
            try
            {
                if (handlers == null || handlers.Length == 0)
                {
                    Debug.Fail(string.Format("Handler集合不应为空."));
                    return;
                }
                if (handlers.Length == 1)
                {
                    handlers[0].Recevied(SessionGetter.Invoke(), protocol);
                }
                else
                {
                    foreach (KnifeSocketProtocolHandler handler in handlers)
                    {
                        if (handler.Commands.Contains(protocol.Command))
                            handler.Recevied(SessionGetter.Invoke(), protocol);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("Handler调用异常:{0}", e.Message), e);
            }
        }
    }
}