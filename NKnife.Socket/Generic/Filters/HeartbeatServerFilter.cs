using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Wrapper;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Exceptions;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : HeartbeatFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        public HeartbeatServerFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000*15;
            IsStrictMode = false;
        }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal override void OnClientCome(SocketSessionEventArgs e)
        {
            base.OnClientCome(e);
            Start();
        }

        protected override void BeatingTimerElapsed(object sender, EventArgs e)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();

            var todoList = new List<EndPoint>(0);//待移除
            foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in map)
            {
                EndPoint endpoint = pair.Key;
                KnifeSocketSession session = pair.Value;
                if (!(session.WaitingForReply)) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
                {
                    try
                    {
                        handlers[0].Write(session, Heartbeat.BeatingOfServerHeart);
                        session.WaitingForReply = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
                    }
                    catch (Exception ex) //发送异常，发不出去则立即移出
                    {
                        _logger.Warn(string.Format("向客户端{0}发送心跳时异常:{1}", endpoint, ex.Message), ex);
                        RemoveEndPointFromSessionMap(endpoint);
                    }
                }
                else
                {
                    todoList.Add(endpoint);
                }
            }
            foreach (EndPoint endPoint in todoList)
            {
                RemoveEndPointFromSessionMap(endPoint);
            }
        }

        protected override byte[] GetReplay()
        {
            return Heartbeat.ReplayOfClient;
        }

        private void RemoveEndPointFromSessionMap(EndPoint endPoint)
        {
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();
            KnifeSocketSession session;
            if (map.TryGetValue(endPoint, out session))
            {
                try
                {
                    if (session.Connector != null && session.Connector.Connected)
                        session.Connector.Close();
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("断开客户端{0}时异常:{1}", endPoint, ex.Message), ex);
                }
                map.Remove(endPoint);
                _logger.Info(string.Format("心跳检查客户端{0}无响应，从SessionMap中移除之。池中:{1}", endPoint, map.Count));
            }
        }
    }

    public abstract class HeartbeatFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        protected HeartbeatFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000 * 15;
            IsStrictMode = false;
        }

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        /// <summary>
        /// 严格模式开关
        /// </summary>
        /// <returns>
        /// true  心跳返回内容一定要和HeartBeat类中定义的ReplayOfClient一致才算有心跳响应
        /// false 心跳返回任何内容均算有心跳相应
        /// </returns>
        public bool IsStrictMode { get; set; }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal void Start()
        {
            if (!_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimerElapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。间隔:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Length > 0, "Handler未设置");
            }
        }

        protected abstract void BeatingTimerElapsed(object sender, EventArgs e);

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {//非严格模式
                session.WaitingForReply = false;
                _logger.Trace(() => string.Format("收到{0}信息,关闭心跳等待.", session.Source));
            }
            if (data.IndexOf(GetReplay()) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("收到{0}心跳回复.", session.Source));
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

        protected abstract byte[] GetReplay();

    }
}