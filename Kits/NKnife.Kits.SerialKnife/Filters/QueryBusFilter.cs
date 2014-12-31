using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SerialKnife.Filters
{
    public class QueryBusFilter : KnifeProtocolProcessorBase<byte[]>, ITunnelFilter<byte[], int>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<int, QueryThreadWrapper> _QueryThreadMap = new Dictionary<int, QueryThreadWrapper>();

        private ISessionProvider<byte[], int> _SessionProvider;
        public void BindSessionProvider(ISessionProvider<byte[], int> sessionProvider)
        {
            _SessionProvider = sessionProvider;
        }

        public bool ContinueNextFilter { get; private set; }

        public QueryBusFilter()
        {
            ContinueNextFilter = true;
        }

        public void PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            
        }

        public void PrcoessSendData(ITunnelSession<byte[], int> session)
        {
            
        }

        public void ProcessSessionBroken(int id)
        {
            if (_QueryThreadMap.ContainsKey(id))
            {
                _QueryThreadMap[id].RunFlag = false;
                _QueryThreadMap[id].QueryThread.Abort();
                _QueryThreadMap.Remove(id);
            }
        }

        public void ProcessSessionBuilt(int id)
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
        private void SendProcess(int id)
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


        private void SendQuery(int id)
        {
            var queryProtocol = Family.Build(new byte[] {0x01}); //命令字
            queryProtocol.CommandParam = new byte[]{0x03,0x00}; //地址，计数
            var data = Family.Generate(queryProtocol);
            var datagram = Codec.Encoder.Execute(data);
            _SessionProvider.Send(id, datagram);
        }


        class QueryThreadWrapper
        {
            public Thread QueryThread { get; set; }
            public bool RunFlag { get; set; }
        }
    }
}
