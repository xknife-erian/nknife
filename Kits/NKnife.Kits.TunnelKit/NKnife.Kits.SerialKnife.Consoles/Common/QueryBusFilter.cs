using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using NKnife.Tunnel.Filters;

namespace NKnife.Kits.SerialKnife.Consoles.Common
{
    public class QueryBusFilter : ProtocolFilter<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<QueryBusFilter>();
        private readonly Dictionary<long, QueryThreadWrapper> _QueryThreadMap = new Dictionary<long, QueryThreadWrapper>();

        public override void ProcessSessionBuilt(long id)
        {
            //连接建立后就开始轮询
            var queryThread = new Thread(QuerySendLoop) { IsBackground = true };
            _QueryThreadMap.Add(id,new QueryThreadWrapper
            {
                QueryThread = queryThread,
                RunFlag = true
            });
            queryThread.Start(id);
        }

        /// <summary>指令发送的循环线程
        /// </summary>
        private void QuerySendLoop(object state)
        {
            var id = (long) state;
            while (_QueryThreadMap[id].RunFlag)
            {
                SendProcess(id);
                Thread.Sleep(500);
            }
        }

        /// <summary>串口数据发送函数
        /// 从数据池中检索数据，没有数据包则直接返回，
        /// 有数据包则先按照数据包中的发送准备时长进行延时等候（PreSleepBeforeSendData），
        /// 然以将数据从串口发出，待数据接收超时后，激发数据包发送完成事件
        /// </summary>
        /// <param name="id"></param>
        private void SendProcess(long id)
        {
            try
            {
                //发送巡查
                SendQuery(id);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                _logger.Warn("SendProcess异常", e);
            }
        }

        private void SendQuery(long id)
        {
            var queryProtocol = _Family.Build(new byte[] {0x01}); //命令字
            queryProtocol.CommandParam = new byte[] {0x03, 0x00}; //地址，计数
            var data = _Family.Generate(queryProtocol);
            var datagram = _Codec.Encoder.Execute(data);
            OnSendToSession(this, new SessionEventArgs(new TunnelSession
            {
                Id = id,
                Data = datagram
            }));
        }
        
        class QueryThreadWrapper
        {
            public Thread QueryThread { get; set; }
            public bool RunFlag { get; set; }
        }
    }
}
