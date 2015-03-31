using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using NKnife.Utility;

namespace NKnife.Kits.SerialKnife.Consoles.Common
{
    public class QueryBusFilter : ITunnelFilter
    {
        private static readonly ILog _logger = LogManager.GetLogger<QueryBusFilter>();
        private readonly Dictionary<long, QueryThreadWrapper> _QueryThreadMap = new Dictionary<long, QueryThreadWrapper>();

        public event EventHandler<SessionEventArgs> SendToSession;
        public event EventHandler<SessionEventArgs> SendToAll;
        public event EventHandler<SessionEventArgs> KillSession;

        public QueryBusFilter()
        {

        }

        public bool PrcoessReceiveData(ITunnelSession session)
        {
            return true;
        }

        public void ProcessSendToSession(ITunnelSession session)
        {
            
        }

        public void ProcessSendToAll(byte[] data)
        {
            
        }

        public void ProcessSessionBroken(long id)
        {
            if (_QueryThreadMap.ContainsKey(id))
            {
                _QueryThreadMap[id].RunFlag = false;
                _QueryThreadMap[id].QueryThread.Abort();
                _QueryThreadMap.Remove(id);
            }
        }

        public void ProcessSessionBuilt(long id)
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
            queryProtocol.CommandParam = new byte[]{0x03,0x00}; //地址，计数
            var data = _Family.Generate(queryProtocol);
            var datagram = _Codec.Encoder.Execute(data);

            var handler = SendToSession;
            if(handler !=null)
                handler.Invoke(this,new SessionEventArgs(new TunnelSession
                {
                    Id = id,
                    Data = datagram
                }));
        }

        #region KnifeProtocolProcessorBase

        protected ITunnelCodec<byte[]> _Codec;
        protected IProtocolFamily<byte[]> _Family;

        public virtual void Bind(ITunnelCodec<byte[]> codec, IProtocolFamily<byte[]> protocolFamily)
        {
            _Codec = codec;
            _logger.Info(string.Format("绑定Codec成功。{0},{1}", _Codec.Decoder.GetType().Name, _Codec.Encoder.GetType().Name));

            _Family = protocolFamily;
            _logger.Info(string.Format("协议族[{0}]绑定成功。", _Family.FamilyName));
        }

        /// <summary>
        ///     数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        public virtual IEnumerable<IProtocol<byte[]>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished)
        {
            if (!UtilityCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", unFinished.Length, srcLen, dataPacket.Length));
            }

            int done;
            byte[][] datagram = _Codec.Decoder.Execute(dataPacket, out done);

            IEnumerable<IProtocol<byte[]>> protocols = null;

            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
            }
            else
            {
                protocols = ParseProtocols(datagram);
            }

            if (dataPacket.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                unFinished = new byte[dataPacket.Length - done];
                Buffer.BlockCopy(dataPacket, done, unFinished, 0, unFinished.Length);
                _logger.Trace(string.Format("半包数据暂存,数据长度:{0}", unFinished.Length));
            }

            return protocols;
        }

        protected virtual IEnumerable<IProtocol<byte[]>> ParseProtocols(byte[][] datagram)
        {
            var protocols = new List<IProtocol<byte[]>>(datagram.Length);
            foreach (var dg in datagram)
            {
                //if (string.IsNullOrWhiteSpace(dg)) 
                //    continue;
                byte[] command;
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

                IProtocol<byte[]> protocol;
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


        class QueryThreadWrapper
        {
            public Thread QueryThread { get; set; }
            public bool RunFlag { get; set; }
        }
    }
}
