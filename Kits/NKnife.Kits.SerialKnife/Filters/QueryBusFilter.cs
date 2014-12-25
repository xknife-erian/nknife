using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SerialKnife.Filters
{
    public class QueryBusFilter : TunnelFilterBase<byte[],int>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private Dictionary<int, QueryThreadWrapper> _QueryThreadMap = new Dictionary<int, QueryThreadWrapper>();

        private ISessionProvider<byte[], int> _SessionProvider;
        public override void BindSessionHandler(ISessionProvider<byte[], int> sessionProvider)
        {
            _SessionProvider = sessionProvider;
        }

        public override void PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            
        }

        public override void ProcessSessionBroken(int id)
        {
            if (_QueryThreadMap.ContainsKey(id))
            {
                _QueryThreadMap[id].RunFlag = false;
                _QueryThreadMap[id].QueryThread.Abort();
                _QueryThreadMap.Remove(id);
            }
        }

        public override void ProcessSessionBuilt(int id)
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
            var id = (int) state;
            while (_QueryThreadMap[id].RunFlag)
            {
                SendProcess();
                Thread.Sleep(1000);
            }
        }

        /// <summary>串口数据发送函数
        /// 从数据池中检索数据，没有数据包则直接返回，
        /// 有数据包则先按照数据包中的发送准备时长进行延时等候（PreSleepBeforeSendData），
        /// 然以将数据从串口发出，待数据接收超时后，激发数据包发送完成事件
        /// </summary>
        private void SendProcess()
        {
            try
            {


            }
            catch (Exception e)
            {
                _logger.Warn("SendProcess异常", e);
            }
        }



        class QueryThreadWrapper
        {
            public Thread QueryThread { get; set; }
            public bool RunFlag { get; set; }
        }
    }
}
