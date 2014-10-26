using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatClientFilter : KnifeSocketClientFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public HeartbeatClientFilter()
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

        protected internal override void OnConnected(ConnectedEventArgs e)
        {
            base.OnConnected(e);
            Start();
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
            KnifeSocketSession session = SessionGetter.Invoke();

            if (!(session.WaitingForReply)) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
            {
                try
                {
                    handlers[0].Write(session, Heartbeat.BeatingOfClientHeart);
                    session.WaitingForReply = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
                }
                catch (Exception ex) //发送异常，发不出去则立即移出
                {
                    _logger.Warn(string.Format("向服务器{0}发送心跳时异常:{1}", session.Source, ex.Message), ex);
                    _logger.Info("准备重连服务器......");
                    Reconnects(session);
                }
            }
            else
            {
                _logger.Warn("在一定时间内，服务器未反应，准备重连服务器......");
                Reconnects(session);
            }
        }

        protected virtual void Reconnects(KnifeSocketSession session)
        {
            OnConnectionBroken(new ConnectionBrokenEventArgs(session.Source, BrokenCause.LoseHeartbeat));
        }

        protected virtual byte[] GetReplay()
        {
            return Heartbeat.ReplayOfServer;
        }

        protected internal virtual void Start()
        {
            if (!_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimerElapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("客户端心跳启动。间隔:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Length > 0, "Handler未设置");
            }
        }

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

    }
}