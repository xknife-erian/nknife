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

        protected Timer _BeatingTimer;
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
            IList<KnifeSocketProtocolHandler> handlers = _HandlersGetter.Invoke();
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
                    _logger.Warn(string.Format("向Server{0}发送心跳时异常:{1}", session.Source, ex.Message), ex);
                    _logger.Info("准备重连Server......");
                    Reconnects(session);
                }
            }
            else
            {
                _logger.Warn(string.Format("Filter在间隔时间({0})内，Server心跳未反应，以事件OnConnectionBroken通知Client实例......", Interval));
                Reconnects(session);
            }
        }

        protected virtual void Reconnects(KnifeSocketSession session)
        {
            OnConnectionBroken(new ConnectionBrokenEventArgs(session.Source, BrokenCause.LoseHeartbeat));
        }

        protected internal override void OnConnectionBroken(ConnectionBrokenEventArgs e)
        {
            base.OnConnectionBroken(e);
            _IsTimerStarted = false;
            if (_BeatingTimer != null)
                _BeatingTimer.Stop(); //关闭心跳，等待重新连接上后，再次启动。
        }

        protected internal virtual void Start()
        {
            if (!_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;

                _BeatingTimer = new Timer();
                _BeatingTimer.Elapsed += BeatingTimerElapsed;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("Client心跳启动。间隔:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handler未设置");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {   //非严格模式
                session.WaitingForReply = false;
                _logger.Trace(() => string.Format("Client收到{0}信息,关闭心跳等待.", session.Source));
            }
            if (data.IndexOf(Heartbeat.BeatingOfServerHeart) == 0)
            {
                _HandlersGetter.Invoke()[0].Write(session, Heartbeat.ReplayOfClient);
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("Client收到{0}心跳.回复完成.", session.Source));
                return;
            } 
            if (data.IndexOf(Heartbeat.ReplayOfServer) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("Client收到{0}心跳回复.", session.Source));
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}