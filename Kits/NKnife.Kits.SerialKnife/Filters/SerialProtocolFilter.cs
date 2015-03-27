using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using NKnife.Utility;

namespace NKnife.Kits.SerialKnife.Filters
{
    public class SerialProtocolFilter : ITunnelProtocolFilter<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialProtocolFilter>();
        private readonly byte[] _CurrentReceiveBuffer = new byte[1024];
        private int _CurrentReceiveByteLength;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        #region Interface

        public bool PrcoessReceiveData(ITunnelSession session)
        {
            byte[] data = session.Data;
            int len = data.Length;
            if (len == 0)
                return false;
            if (_CurrentReceiveByteLength + len > 1024) //缓冲区溢出了，只保留后1024位
            {
                //暂时不做处理，直接抛弃
                _logger.Warn("收到的数据超出1024，缓冲区溢出，该条数据抛弃");
                return false;
            }

            var tempData = new byte[_CurrentReceiveByteLength + len];
            Array.Copy(_CurrentReceiveBuffer, 0, tempData, 0, _CurrentReceiveByteLength);
            Array.Copy(data, 0, tempData, _CurrentReceiveByteLength, data.Length);
            var unfinished = new byte[] {};
            var protocols = ProcessDataPacket(tempData, unfinished);

            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _CurrentReceiveBuffer, 0, unfinished.Length);
                _CurrentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _CurrentReceiveByteLength = 0;
            }

            if (protocols != null)
            {
                EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> handler = ProtocolsReceived;
                if (handler != null)
                {
                    handler.Invoke(this, new EventArgs<IEnumerable<IProtocol<byte[]>>>(protocols));
                }
            }
            return true;
        }

        public void ProcessSendToSession(ITunnelSession session)
        {
        }

        public void ProcessSendToAll(byte[] data)
        {
        }

        public event EventHandler<SessionEventArgs> SendToSession;
        public event EventHandler<SessionEventArgs> SendToAll;
        public event EventHandler<SessionEventArgs> KillSession;

        public void ProcessSessionBroken(long id)
        {
        }

        public void ProcessSessionBuilt(long id)
        {
        }

        #endregion

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


        public void AddHandlers(params ITunnelProtocolHandler<byte[]>[] handlers)
        {
            throw new NotImplementedException();
        }

        public void RemoveHandler(ITunnelProtocolHandler<byte[]> handler)
        {
            throw new NotImplementedException();
        }
    }
}