using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Common.Logging;
using NKnife.Interface;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using NKnife.Utility;
using SocketKnife.Events;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketFilter : ISocketFilter
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected KnifeSocketFilter()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #region Equals

        private readonly string _Id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KnifeSocketFilter) obj);
        }

        protected bool Equals(KnifeSocketFilter other)
        {
            return string.Equals(_Id, other._Id);
        }

        public override int GetHashCode()
        {
            return (_Id != null ? _Id.GetHashCode() : 0);
        }

        #endregion

        protected Func<StringProtocolFamily> _FamilyGetter;
        protected Func<IList<KnifeSocketProtocolHandler>> _HandlersGetter;
        protected Func<KnifeSocketCodec> _CodecGetter;

        public abstract bool ContinueNextFilter { get; }

        void ITunnelFilter<EndPoint, Socket>.PrcoessReceiveData(ITunnelSession<EndPoint, Socket> session, byte[] data)
        {
            PrcoessReceiveData((KnifeSocketSession) session, data);
        }

        public event EventHandler<DataFetchedEventArgs<EndPoint>> DataFetched;

        public event EventHandler<DataDecodedEventArgs<EndPoint>> DataDecoded;

        public virtual void BindGetter(Func<KnifeSocketCodec> codecFunc, Func<IList<KnifeSocketProtocolHandler>> handlerGetter, Func<StringProtocolFamily> familyGetter)
        {
            _FamilyGetter = familyGetter;
            _HandlersGetter = handlerGetter;
            _CodecGetter = codecFunc;
            OnBoundGetter();
        }

        /// <summary>
        /// 当核心对象获取器绑定完成时发生
        /// </summary>
        protected virtual void OnBoundGetter()
        {
        }

        public abstract void PrcoessReceiveData(KnifeSocketSession session, byte[] data);

        protected internal virtual void OnDataFetched(SocketDataFetchedEventArgs e)
        {
            EventHandler<DataFetchedEventArgs<EndPoint>> handler = DataFetched;
            if (handler != null) 
                handler(this, e);
        }

        protected internal virtual void OnDataDecoded(SocketDataDecodedEventArgs e)
        {
            EventHandler<DataDecodedEventArgs<EndPoint>> handler = DataDecoded;
            if (handler != null) 
                handler(this, e);
        }

        /// <summary>
        /// 数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <param name="dataDecoder">处理过程中的解码动作</param>
        /// <param name="endPoint">解码动作所需要的参数</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        protected virtual byte[] ProcessDataPacket(byte[] dataPacket, byte[] unFinished, Func<EndPoint, byte[], int> dataDecoder, EndPoint endPoint)
        {
            if (!UtilityCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", unFinished.Length, srcLen, dataPacket.Length));
                unFinished = new byte[] {};
            }
            int done = dataDecoder(endPoint, dataPacket);
            if (dataPacket.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                unFinished = new byte[dataPacket.Length - done];
                Buffer.BlockCopy(dataPacket, done, unFinished, 0, unFinished.Length);
                _logger.Trace(string.Format("半包数据暂存,数据长度:{0}", unFinished.Length));
            }
            return unFinished;
        }
    }
}