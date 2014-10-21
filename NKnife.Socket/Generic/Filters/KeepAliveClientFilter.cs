using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected internal override void OnConnectioned(ConnectionedEventArgs e)
        {
            base.OnConnectioned(e);
            //当连接启动后，启动数据池监听线程
            var thread = new Thread(ReceiveQueueMonitor);
            thread.Start();
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
            var nodone = new byte[] { };
            while (_EnableReceiveQueueMonitor)
            {
                if (_ReceiveQueue.Count > 0)
                {
                    byte[] data = _ReceiveQueue.Dequeue();
                    GetDataList(ref nodone, ref data);
                }
                else
                {
                    _ReceiveQueue.AutoResetEvent.WaitOne();
                }
            }
        }


        protected virtual void GetDataList(ref byte[] nodone, ref byte[] data)
        {
            if (!UtilityCollection.IsNullOrEmpty(nodone))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = data.Length;
                var list = new List<byte>(data.Length + nodone.Length);
                list.AddRange(nodone);
                list.AddRange(data);
                data = list.ToArray();
                _logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", nodone.Length, srcLen, data.Length));
                nodone = new byte[] { };
            }
            int done;
            DataProcessBase(null, data, out done);
            if (data.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                nodone = new byte[data.Length - done];
                Buffer.BlockCopy(data, done, nodone, 0, nodone.Length);
                _logger.Trace(string.Format("半包数据暂存,数据长度:{0}", nodone.Length));
            }
        }

        /// <summary>
        ///     处理协议数据
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        /// <param name="done"></param>
        protected virtual void DataProcessBase(EndPoint endpoint, byte[] data, out int done)
        {
            var codec = _CodecGetter.Invoke();
            string[] datagram = codec.SocketDecoder.Execute(data, out done);
            OnDataDecoded(new SocketDataDecodedEventArgs(endpoint, datagram));
            if (UtilityCollection.IsNullOrEmpty(datagram))
                return;

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
                }
            }
        }

        /// <summary>
        ///     // 触发数据基础解析后发生的数据到达事件
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
//                if (handlers.Length == 1)
//                {
//                    handlers[0].Recevied(session, protocol);
//                }
//                else
//                {
//                    foreach (KnifeSocketProtocolHandler handler in handlers)
//                    {
//                        if (handler.Commands.Contains(protocol.Command))
//                            handler.Recevied(session, protocol);
//                    }
//                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("handler调用异常:{0}", e.Message), e);
            }
        }
    }
}